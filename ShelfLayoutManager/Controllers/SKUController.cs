using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;

namespace ShelfLayoutManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SKUController : ControllerBase
    {
        private readonly ISKURepository _skuRepository;
        private readonly ILogger<SKUController> _logger;

        public SKUController(ISKURepository skuRepository, ILogger<SKUController> logger)
        {
            _skuRepository = skuRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SKU>), 200)]
        public async Task<ActionResult<IEnumerable<SKU>>> GetSKUs()
        {
            var skus = await _skuRepository.GetAllSKUsAsync();
            if (skus == null || !skus.Any())
            {
                _logger.LogInformation("No skus found.");
                return NotFound();
            }
            return Ok(skus);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SKU), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SKU>> GetSKU(int id)
        {
            var sku = await _skuRepository.GetSKUByIdAsync(id);
            if (sku == null)
            {
                _logger.LogInformation("NotFound");
                return NotFound();
            }
            return Ok(sku);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SKU), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SKU>> PostSKU(SKU sku)
        {
            var createdSku = await _skuRepository.CreateSKUAsync(sku);
            return CreatedAtAction("GetSKU", new { id = createdSku.Id }, createdSku);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutSKU(int id, SKU sku)
        {
            try
            {
                var updatedSku = await _skuRepository.UpdateSKUAsync(id, sku);
                if (updatedSku == null)
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occured when Updating sku", ex);
                return StatusCode(500, ex);
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteSKU(int id)
        {
            var result = await _skuRepository.DeleteSKUAsync(id);
            if (!result)
            {
                _logger.LogInformation("sku not found for deletion.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation("sku deleted successfully.");
                return NoContent();
            }
            
        }
    }
}


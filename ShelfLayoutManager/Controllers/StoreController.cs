using Microsoft.AspNetCore.Mvc;
using ShelfLayoutManager.Model;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;
using Microsoft.Extensions.Logging;
using ShelfLayoutManager.Entity;

namespace ShelfLayoutManager.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IStoreRepository storeRepository, ILogger<StoreController> logger)
        {
            _storeRepository = storeRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoreModel>), 200)]
        public async Task<ActionResult<IEnumerable<StoreModel>>> GetStores()
        {
            var stores = await _storeRepository.GetStoresAsync();
            return Ok(stores);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StoreModel), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<StoreModel>> GetStore(int id)
        {
            var store = await _storeRepository.GetStoreAsync(id);
            if (store != null)
            {
                return Ok(store);
            }
            _logger.LogInformation("Store not found");
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoreModel), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<StoreModel>> PostStore(StoreModel store)
        {
            if (store != null)
            {
                var createdStore = await _storeRepository.CreateStoreAsync(new Store
                {
                    Name = store.Name,
                    Location = store.Location
                });
                return CreatedAtAction(nameof(GetStore), new { id = createdStore.Id }, createdStore);
            }
            _logger.LogError("Invalid StoreModel.");
            return BadRequest("Invalid StoreModel");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutStore(int id, StoreModel store)
        {
            if (store != null)
            {
                var updatedStore = await _storeRepository.UpdateStoreAsync(id, store);
                if (updatedStore != null)
                {
                    return NoContent();
                }
                _logger.LogInformation("Store not found");
                return NotFound();
            }
            _logger.LogError("Invalid StoreModel.");
            return BadRequest("Invalid StoreModel");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteStore(int id)
        {
            bool result = await _storeRepository.DeleteStoreAsync(id);
            if (result)
            {
                return NoContent();
            }
            _logger.LogInformation("Store not found");
            return NotFound();
        }
    }
}

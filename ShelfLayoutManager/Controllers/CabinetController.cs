using Microsoft.AspNetCore.Mvc;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;


namespace ShelfLayoutManager.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetController : ControllerBase
    {
        private readonly ICabinetRepository _cabinetRepository;
        private readonly ILogger<CabinetController> _logger;

        public CabinetController(ICabinetRepository cabinetRepository, ILogger<CabinetController> logger)
        {
            _cabinetRepository = cabinetRepository;
            _logger = logger;
        }

        // GET: api/Cabinet
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CabinetModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CabinetModel>>> GetCabinets()
        {
            var cabinets = await _cabinetRepository.GetCabinetsAsync();

            if (cabinets == null || !cabinets.Any())
            {
                _logger.LogInformation("No cabinets found.");
                return NotFound();
            }

            var cabinetModels = cabinets.Select(cabinet => cabinet).ToList();
            return cabinetModels;
        }

        // GET: api/Cabinet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CabinetModel>> GetCabinet(int id)
        {
            var cabinet = await _cabinetRepository.GetCabinetAsync(id);
            if (cabinet == null)
            {
                _logger.LogInformation("NotFound");
                return NotFound();
            }

            var cabinetModel = cabinet;
            return cabinetModel;
        }

        // POST: api/Cabinet
        [HttpPost]
        [HttpPost]
        [ProducesResponseType(typeof(CabinetModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CabinetModel>> PostCabinet(CabinetModel cabinetModel)
        {
            if (cabinetModel == null)
            {
                _logger.LogError("Invalid CabinetModel.");
                return BadRequest("Invalid CabinetModel");
            }

            // Map the CabinetModel to the Cabinet entity
            var cabinet = new Cabinet
            {
                PositionX = cabinetModel.PositionX,
                PositionY = cabinetModel.PositionY,
                PositionZ = cabinetModel.PositionZ,
                Width = cabinetModel.Width,
                Depth = cabinetModel.Depth,
                Height = cabinetModel.Height,
                Number = cabinetModel.Number
            };

            // Add the cabinet to the context and save changes
            await _cabinetRepository.CreateCabinetAsync(cabinet);

            // Map the created cabinet back to a CabinetModel and return it
            var createdCabinetModel = new CabinetModel
            {
                Number = cabinet.Number,
                PositionX = cabinet.PositionX,
                PositionY = cabinet.PositionY,
                PositionZ = cabinet.PositionZ,
                Width = cabinet.Width,
                Depth = cabinet.Depth,
                Height = cabinet.Height,
            };
            _logger.LogInformation("Cabinet Created Successfully");
            return CreatedAtAction("GetCabinet", new { id = cabinet.Number }, createdCabinetModel);
        }

        // PUT: api/Cabinet/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCabinet(int number, CabinetModel cabinetModel)
        {
            if (cabinetModel == null)
            {
                _logger.LogError("Invalid CabinetModel for update.");
                return BadRequest("Invalid CabinetModel for update.");
            }

            try
            {
                await _cabinetRepository.UpdateCabinetAsync(number, cabinetModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred when updating Cabinet.", ex);
                return StatusCode(500, "An error occurred when updating the cabinet.");
            }

        }

        // DELETE: api/Cabinet/5
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCabinet(int number)
        {
            bool result = await _cabinetRepository.DeleteCabinetAsync(number);

            if (result)
            {
                _logger.LogInformation("Cabinet deleted successfully.");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Cabinet not found for deletion.");
                return NotFound();
            }
        }
    }
}


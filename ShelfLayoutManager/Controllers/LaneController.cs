using Microsoft.AspNetCore.Mvc;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;

namespace ShelfLayoutManager.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LaneController : ControllerBase
    {
        private readonly ILaneRepository _laneRepository;
        private readonly ILogger<CabinetController> _logger;

        public LaneController(ILaneRepository laneRepository, ILogger<CabinetController> logger)
        {
            _laneRepository = laneRepository;
            _logger = logger;
        }

        // GET: api/Lane
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LaneModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<LaneModel>>> GetLanes()
        {
            var lanes = await _laneRepository.GetAllLanesAsync();
            if (lanes == null || !lanes.Any())
            {
                _logger.LogInformation("No lanes found.");
                return NotFound();
            }
            var laneModels = lanes.Select(lane => MapLaneToModel(lane)).ToList();
            return laneModels;
        }

        // GET: api/Lane/5
        [HttpGet("{number}")]
        [ProducesResponseType(typeof(LaneModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LaneModel>> GetLane(int number)
        {
            var lane = await _laneRepository.GetLaneByIdAsync(number);
            if (lane == null)
            {
                _logger.LogInformation("NotFound");
                return NotFound();
            }
            var laneModel = MapLaneToModel(lane);
            return laneModel;
        }

        // PUT: api/Lane/5
        [HttpPut("{number}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutLane(int number, LaneModel laneModel)
        {
            if (laneModel == null)
            {
                _logger.LogError("Invalid LaneModel. Request body is null.");
                return BadRequest("Invalid LaneModel. Request body is null.");
            }

            try
            {
                var lane = MapModelToLane(laneModel);
                await _laneRepository.UpdateLaneAsync(lane);
            }
           
            catch (Exception ex)
            {
                _logger.LogError("Exception Occured when Updating Lane", ex);
                return StatusCode(500,ex);
            }

            return NoContent();
        }

        // POST: api/Lane
        [HttpPost]
        [ProducesResponseType(typeof(LaneModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LaneModel>> PostLane(LaneModel laneModel)
        {
            if (laneModel == null)
            {
                _logger.LogError("Invalid LaneModel. Request body is null.");
                return BadRequest("Invalid LaneModel. Request body is null.");
            }

            var lane = MapModelToLane(laneModel);
            await _laneRepository.CreateLaneAsync(lane);

            var createdLaneModel = new LaneModel
            {
                Number = lane.Number,
                JanCode = lane.JanCode,
                Quantity = lane.Quantity,
                PositionX = lane.PositionX,
                RowId = lane.RowId,
            };

            return CreatedAtAction("GetLane", new { number = lane.Number }, createdLaneModel);
        }

        // DELETE: api/Lane/5
        [HttpDelete("{number}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLane(int number)
        {
            if (number <= 0)
            {
                _logger.LogError("Invalid request. Number in URL is invalid.");
                return BadRequest("Invalid request. Number in URL is invalid.");
            }

            bool result = await _laneRepository.DeleteLaneAsync(number);

            if (result)
            {
                _logger.LogInformation("Lane deleted successfully.");
                return Ok();
            }
            else
            {
                _logger.LogInformation("Lane not found for deletion.");
                return NotFound();
            }
        }

        private LaneModel MapLaneToModel(Lane lane)
        {
            return new LaneModel
            {
                Number = lane.Number,
                JanCode = lane.JanCode,
                Quantity = lane.Quantity,
                PositionX = lane.PositionX,
                RowId = lane.RowId 
            };
        }

        private Lane MapModelToLane(LaneModel laneModel)
        {
            return new Lane
            {
                Number = laneModel.Number,
                JanCode = laneModel.JanCode,
                Quantity = laneModel.Quantity,
                PositionX = laneModel.PositionX,
                RowId = laneModel.RowId
            };
        }
    }
}


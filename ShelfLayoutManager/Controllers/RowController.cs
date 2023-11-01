using Microsoft.AspNetCore.Mvc;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;

namespace ShelfLayoutManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RowController : ControllerBase
    {
        private readonly IRowRepository _rowRepository;
        private readonly ILogger<CabinetController> _logger;

        public RowController(IRowRepository rowRepository, ILogger<CabinetController> logger)
        {
            _rowRepository = rowRepository;
            _logger = logger;
        }

        // GET: api/Row
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RowModel>>> GetRows()
        {

            var rows = await _rowRepository.GetAllRowsAsync();
            if (rows == null || !rows.Any())
            {
                _logger.LogInformation("No rows found.");
                return NotFound();
            }
            var rowModels = rows.Select(row => MapModelToRow(row)).ToList();
            return rowModels;
           
        }

        // GET: api/Row/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RowModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Row>> GetRow(int number)
        {
            var row = await _rowRepository.GetRowByIdAsync(number);

            if (row == null)
            {
                _logger.LogInformation("Row not found.");
                return NotFound();
            }

            return row;
        }

        // POST: api/Row
        [HttpPost]
        [ProducesResponseType(typeof(RowModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RowModel>> PostRow(RowModel rowModel)
        {
            if (rowModel == null)
            {
                _logger.LogError("Invalid RowModel. Request body is null.");
                return BadRequest("Invalid RowModel. Request body is null.");
            }

            var row = new Row
            {
                Number = rowModel.Number,
                PositionZ = rowModel.PositionZ,
                Height = rowModel.Height,
                CabinetId = rowModel.CabinetId,
            };
            
            await _rowRepository.CreateRowAsync(row);

            var createdRowModel = new RowModel
            {
                Number = row.Number,
                PositionZ = row.PositionZ,
                CabinetId = row.CabinetId,
                Height = row.Height,
            };

            return CreatedAtAction("GetRow", new { id = row.Number }, createdRowModel);
        }

        // PUT: api/Row/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutRow(int number, RowModel rowModel)
        {
            if (rowModel == null)
            {
                _logger.LogError("Invalid RowModel. Request body is null.");
                return BadRequest("Invalid RowModel. Request body is null.");
            }
            var row = new Row
            {
                Number = rowModel.Number,
                PositionZ = rowModel.PositionZ,
                Height = rowModel.Height,
                CabinetId = rowModel.CabinetId,
            };

            try
            {
                await _rowRepository.UpdateRowAsync(row);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred when updating Row.", ex);
                return StatusCode(500, ex);
            }

            return NoContent();
        }

        // DELETE: api/Row/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRow(int number)
        {
           var result = await _rowRepository.DeleteRowAsync(number);


            if (result)
            {
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Row not found.");
                return NotFound();
            }
        }

        private RowModel? MapModelToRow(Row rowModel)
        {
            if (rowModel == null)
            {
                return null;
            }

            // Create a new Row entity and map the properties
            var newRowModel = new RowModel
            {
                Number = rowModel.Number,
                PositionZ = rowModel.PositionZ,
                Height = rowModel.Height,
                CabinetId = rowModel.CabinetId,
            };

            return newRowModel;
        }
    }
}


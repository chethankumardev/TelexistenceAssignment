using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using ShelfLayoutManager.Controllers;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;
using Xunit;
using Moq.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;
using Serilog;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace ShelfLayoutManager.Tests.ControllerTests
{
    public class CabinetControllerTests
    {
        private ICabinetRepository _cabinetRepository;
        private ILogger<CabinetController> _logger;
        private ShelfLayoutDbContext _context;

        public CabinetControllerTests()
        {
            var options = new DbContextOptionsBuilder<ShelfLayoutDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ShelfLayoutDbContext(options);
            var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog());
            _logger = loggerFactory.CreateLogger<CabinetController>();

            _cabinetRepository = new CabinetRepository(_context);
        }

        [Fact]
        public async Task Test_GetCabinets_WithData()
        {
            // Arrange
            var controller = new CabinetController(_cabinetRepository, _logger);
            SeedCabinetData();

            // Act
            var result = await controller.GetCabinets();
            var cabinetModels = result.Value as IEnumerable<CabinetModel>;

            // Assert
            Assert.NotNull(cabinetModels);
            Assert.Equal(2, cabinetModels.Count()); 
        }


        [Fact]
        public async Task Test_GetCabinet_Exists()
        {
            // Arrange
            var controller = new CabinetController(_cabinetRepository, _logger);
            SeedCabinetData();
            var expectedCabinet = new CabinetModel
            {
                Number = 1,
                PositionX = 10,
                PositionY = 20,
                PositionZ = 0,
                Width = 100,
                Depth = 50,
                Height = 200
            };

            // Act
            var result = await controller.GetCabinet(1);
            var cabinetModel = result.Value as CabinetModel;

            // Assert
            Assert.NotNull(cabinetModel);
            Assert.Equal(expectedCabinet.Number, cabinetModel.Number);
            Assert.Equal(expectedCabinet.PositionX, cabinetModel.PositionX);
        }

        [Fact]
        public async Task Test_GetCabinet_NotFound()
        {
            // Arrange
            var controller = new CabinetController(_cabinetRepository, _logger);

            // Act
            var result = await controller.GetCabinet(999); 
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode); 
        }


        [Fact]
        public async Task Test_PostCabinet_Created()
        {
            // Arrange
            var controller = new CabinetController(_cabinetRepository, _logger);
            var newCabinet = new CabinetModel
            {
                PositionX = 15,
                PositionY = 25,
                PositionZ = 5,
                Width = 80,
                Depth = 60,
                Height = 220
            };

            // Act
            var result = await controller.PostCabinet(newCabinet);
            var createdResult = result.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode); // 201 Created
            var createdCabinet = createdResult.Value as CabinetModel;
            Assert.NotNull(createdCabinet);
            
        }

        [Fact]
        public async Task Test_PutCabinet_Updated()
        {
            // Arrange
            var controller = new CabinetController(_cabinetRepository, _logger);
            SeedCabinetData();
            var updatedCabinet = new CabinetModel
            {
                Number = 1,
                PositionX = 15,
                PositionY = 25,
                PositionZ = 5,
                Width = 80,
                Depth = 60,
                Height = 220
                
            };

            // Act
            var result = await controller.PutCabinet(1, updatedCabinet);
            var noContentResult = result as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode); // 204 No Content
                                                           
        }

        [Fact]
        public async Task Test_DeleteCabinet_Deleted()
        {
            // Arrange
            var controller = new CabinetController(_cabinetRepository, _logger);
            SeedCabinetData();

            // Act
            var result = await controller.DeleteCabinet(1);

            // AssertRes
            var statusCodeResult = result as NoContentResult;
            Assert.NotNull(statusCodeResult);
            Assert.Equal(StatusCodes.Status204NoContent, statusCodeResult.StatusCode); // 204 No Content

        }

        [Fact]
        public async Task Test_DeleteCabinet_NotFound()
        {
            // Arrange
            var controller = new CabinetController(_cabinetRepository, _logger);

            // Act
            var result = await controller.DeleteCabinet(999); 
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode); // 404 Not Found
        }

        private void SeedCabinetData()
        {
            var cabinets = new List<Cabinet>
            {
                new Cabinet
                {
                    PositionX = 10,
                    PositionY = 20,
                    PositionZ = 0,
                    Width = 100,
                    Depth = 50,
                    Height = 200
                },
                new Cabinet
                {
                    PositionX = 15,
                    PositionY = 25,
                    PositionZ = 5,
                    Width = 80,
                    Depth = 60,
                    Height = 220
                }
            };

            _context.Cabinets.AddRange(cabinets);
            _context.SaveChanges();
        }

    }
}



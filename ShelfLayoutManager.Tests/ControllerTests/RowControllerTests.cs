using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ShelfLayoutManager.Controllers;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;
using ShelfLayoutManager.RepositoriesInterface.Repositories;
using Xunit;

namespace ShelfLayoutManager.Tests.ControllerTests
{
    public class RowControllerTests
    {

        private readonly Mock<IRowRepository> _rowRepositoryMock;
        private readonly RowController _controller;

        public RowControllerTests()
        {
            _rowRepositoryMock = new Mock<IRowRepository>();
            var logger = new Mock<ILogger<CabinetController>>().Object;
            _controller = new RowController(_rowRepositoryMock.Object, logger);
        }

        [Fact]
        public async Task Test_GetRows_WithData()
        {
            // Arrange
            var rows = new List<Row>
            {
                new Row { PositionZ = 50, Height = 40, CabinetId = 1 },
                new Row { PositionZ = 55, Height = 35, CabinetId = 1 }
            };

            _rowRepositoryMock.Setup(repo => repo.GetAllRowsAsync()).ReturnsAsync(rows);

            // Act
            var result = await _controller.GetRows();
            var okResult = result.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(2, okResult.Count()); 
        }

        [Fact]
        public async Task Test_GetRow_Exists()
        {
            // Arrange
           
            var expectedRow = new Row
            {
                Number = 1,
                PositionZ = 5,
                Height = 10,
                CabinetId = 1
                
            };

          
            _rowRepositoryMock.Setup(repo => repo.GetRowByIdAsync(1)).ReturnsAsync(expectedRow);

            // Act
            var result = await _controller.GetRow(1);
            var okResult = result.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(expectedRow.Number, okResult.Number);
            Assert.Equal(expectedRow.PositionZ, okResult.PositionZ);
            
        }

        [Fact]
        public void Test_GetRow_NotFound()
        {
            // Arrange
            _rowRepositoryMock.Setup(repo => repo.GetRowByIdAsync(1)).ReturnsAsync((Row)null);

            // Act
            var result = _controller.GetRow(1);
            var okResult = result.Result.Value;

            // Assert
            Assert.Null(okResult);
        }

        [Fact]
        public void Test_PostRow_Created()
        {
            // Arrange
            var newRow = new Row
            {
                PositionZ = 50,
                Height = 40,
                CabinetId = 1
               
            };
            
            _rowRepositoryMock.Setup(repo => repo.CreateRowAsync(newRow));

            // Act
            var result = _controller.PostRow(MapModelToRow(newRow));
            var okResult = result.Result;

            // Assert
            Assert.NotNull(okResult);
           
        }

        [Fact]
        public void Test_PutRow_Updated()
        {
            // Arrange
            var updatedRow = new RowModel
            {
                Number = 1,
                PositionZ = 60,
                Height = 45,
                CabinetId = 1
                // Add other properties
            };

            // Act
            var result = _controller.PutRow(1, updatedRow);
            var okResult = result.Result;

            // Assert
            Assert.IsType<NoContentResult>(okResult);
            
        }

        public RowModel? MapModelToRow(Row rowModel)
        {
            if (rowModel == null)
            {
                return null;
            }

           
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

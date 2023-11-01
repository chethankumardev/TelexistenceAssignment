using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ShelfLayoutManager.Controllers;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;
using ShelfLayoutManager.RepositoriesInterface.Repositories;
using Xunit;

namespace ShelfLayoutManager.Tests.ControllerTests
{
    public class LaneControllerTests
    {
        private readonly Mock<ILaneRepository> _laneRepositoryMock;
        private readonly LaneController _controller;

        public LaneControllerTests()
        {
            _laneRepositoryMock = new Mock<ILaneRepository>();
            var logger = new Mock<ILogger<CabinetController>>().Object;
            _controller = new LaneController(_laneRepositoryMock.Object, logger);
        }


        [Fact]
        public void Test_GetLanes_WithData()
        {
            // Arrange
            var lanes = new List<Lane>
            {
                new Lane { JanCode = "ABC123", Quantity = 10, PositionX = 5, RowId = 1 },
                new Lane { JanCode = "DEF456", Quantity = 15, PositionX = 8, RowId = 1 }
            };

            _laneRepositoryMock.Setup(repo => repo.GetAllLanesAsync()).ReturnsAsync(lanes);

            // Act
            var result = _controller.GetLanes();
            var okResult = result.Result.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(2, okResult.Count()); 
        }

        [Fact]
        public void Test_GetLane_Exists()
        {
            // Arrange
            var lane = new Lane
            {
                Number = 1,
                JanCode = "ABC123",
                Quantity = 10,
                PositionX = 5,
                RowId = 1
            };

            _laneRepositoryMock.Setup(repo => repo.GetLaneByIdAsync(1)).ReturnsAsync(lane);

            // Act
            var result = _controller.GetLane(1);
            var okResult = result.Result.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(lane.Number, okResult.Number);
            Assert.Equal(lane.JanCode, okResult.JanCode);
            
        }

        [Fact]
        public void Test_GetLane_NotFound()
        {
            // Arrange
            _laneRepositoryMock.Setup(repo => repo.GetLaneByIdAsync(1)).ReturnsAsync((Lane)null);

            // Act
            var result = _controller.GetLane(1);
            var okResult = result.Result.Value;

            // Assert
            Assert.Null(okResult);
        }

        [Fact]
        public void Test_PostLane_Created()
        {
            // Arrange
            var newLane = new LaneModel
            {
                JanCode = "XYZ789",
                Quantity = 20,
                PositionX = 10,
                RowId = 1,
                Number = 1
               
            };
            var lane = MapModelToLane(newLane);
            _laneRepositoryMock.Setup(repo => repo.CreateLaneAsync(lane));

            // Act
            var result = _controller.PostLane(newLane);
            var okResult = result;

            // Assert
            Assert.NotNull(okResult);
            
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

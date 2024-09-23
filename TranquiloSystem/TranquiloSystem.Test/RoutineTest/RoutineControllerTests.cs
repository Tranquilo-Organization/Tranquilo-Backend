using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.API.Controllers;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.RoutineDto;
using TranquiloSystem.BLL.Manager.RoutineManager;

namespace TranquiloSystem.Test.RoutineTest
{
    public class RoutineControllerTests
    {
        private readonly Mock<IRoutineManager> _routineManagerMock;
        private readonly RoutineController _controller;

        public RoutineControllerTests()
        {
            _routineManagerMock = new Mock<IRoutineManager>();
            _controller = new RoutineController(_routineManagerMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenRoutinesExist()
        {
            var responseDto = new GeneralResponseDto { IsSucceeded = true, Model = new List<RoutineReadDto>(), StatusCode = 200 };
            _routineManagerMock.Setup(manager => manager.GetAllAsync()).ReturnsAsync(responseDto);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenRoutineDoesNotExist()
        {
            var responseDto = new GeneralResponseDto { IsSucceeded = false, Message = "No routine with this id", StatusCode = 404 };
            _routineManagerMock.Setup(manager => manager.GetByIdAsync(1)).ReturnsAsync(responseDto);

            var result = await _controller.GetById(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetByLevelIdAll_ShouldReturnOk_WhenRoutinesExistForLevel()
        {
            var responseDto = new GeneralResponseDto { IsSucceeded = true, Model = new List<RoutineReadDto>(), StatusCode = 200 };
            _routineManagerMock.Setup(manager => manager.GetByLevelIdAsync(1)).ReturnsAsync(responseDto);

            var result = await _controller.GetByLevelIdAll(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        } 
    }
}

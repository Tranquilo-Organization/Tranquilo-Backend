using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using Tranquilo.DAL.Repositories.RoutineRepo;
using TranquiloSystem.BLL.Dtos.RoutineDto;
using TranquiloSystem.BLL.Manager.RoutineManager;

namespace TranquiloSystem.Test.RoutineTest
{
    public class RoutineManagerTests
    {
        private readonly Mock<IRoutineRepository> _routineRepositoryMock;
        private readonly IMapper _mapper;
        private readonly RoutineManager _routineManager;

        public RoutineManagerTests()
        {
            _routineRepositoryMock = new Mock<IRoutineRepository>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Routine, RoutineReadDto>());
            _mapper = config.CreateMapper();
            _routineManager = new RoutineManager(_routineRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnRoutines_WhenRoutinesExist()
        {
            var routines = new List<Routine>
        {
            new Routine { Id = 1, Name = "Routine 1", Description = "Description 1", Type = "morning", Steps = "Step 1\nStep 2", Level = new Level { Name = "Level 1" } },
            new Routine { Id = 2, Name = "Routine 2", Description = "Description 2", Type = "evening", Steps = "Step A\nStep B", Level = new Level { Name = "Level 2" } }
        };
            _routineRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(routines);

            var result = await _routineManager.GetAllAsync();

            Assert.True(result.IsSucceeded);
            Assert.IsType<List<RoutineReadDto>>(result.Model);
            Assert.Equal(2, ((List<RoutineReadDto>)result.Model).Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRoutine_WhenRoutineExists()
        {
            var routine = new Routine { Id = 1, Name = "Routine 1", Description = "Description 1", Type = "morning", Steps = "Step 1\nStep 2", Level = new Level { Name = "Level 1" } };
            _routineRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(routine);

            var result = await _routineManager.GetByIdAsync(1);

            Assert.True(result.IsSucceeded);
            Assert.IsType<RoutineReadDto>(result.Model);
        }

        [Fact]
        public async Task GetByLevelIdAsync_ShouldReturnRoutines_WhenRoutinesExistForLevel()
        {
            var routines = new List<Routine>
        {
            new Routine { Id = 1, Name = "Routine 1", Description = "Description 1", Type = "morning", Steps = "Step 1\nStep 2", LevelId = 1, Level = new Level { Name = "Level 1" } },
            new Routine { Id = 2, Name = "Routine 2", Description = "Description 2", Type = "evening", Steps = "Step A\nStep B", LevelId = 1, Level = new Level { Name = "Level 1" } }
        };
            _routineRepositoryMock.Setup(repo => repo.GetByLevelIdAsync(1)).ReturnsAsync(routines);

            var result = await _routineManager.GetByLevelIdAsync(1);

            Assert.True(result.IsSucceeded);
            Assert.IsType<List<RoutineReadDto>>(result.Model);
            Assert.Equal(2, ((List<RoutineReadDto>)result.Model).Count);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnNotFound_WhenRoutineDoesNotExist()
        {
            // Arrange
            _routineRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Routine)null);

            // Act
            var result = await _routineManager.GetByIdAsync(1);

            // Assert
            Assert.False(result.IsSucceeded);
            Assert.Null(result.Model);
            Assert.Equal("No routine with this id", result.Message);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoRoutinesExist()
        {
            // Arrange
            var routines = new List<Routine>();
            _routineRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(routines);

            // Act
            var result = await _routineManager.GetAllAsync();

            // Assert
            Assert.True(result.IsSucceeded);
            Assert.IsType<List<RoutineReadDto>>(result.Model);
            Assert.Empty((List<RoutineReadDto>)result.Model);
        }

    }
}


using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.API.Controllers;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.ProfileDto;
using TranquiloSystem.BLL.Manager.ProfileManager;

namespace TranquiloSystem.Test.Profiletest
{
    public class ProfileControllerTests
    {
        private readonly Mock<IProfileManager> _mockProfileManager;
        private readonly ProfileController _controller;

        public ProfileControllerTests()
        {
            _mockProfileManager = new Mock<IProfileManager>();
            _controller = new ProfileController(_mockProfileManager.Object);
        }


        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var userId = "validUserId";
            var expectedProfile = new ProfileReadDto { Id = "id", Email = "@gmail", UserName = "name" };

            _mockProfileManager.Setup(m => m.GetUsersByIdAsync(userId))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = true, Model = expectedProfile });

            // Act
            var result = await _controller.GetById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var userId = "invalid";

            _mockProfileManager.Setup(m => m.GetUsersByIdAsync(userId))
                .ReturnsAsync(new GeneralResponseDto
                {
                    IsSucceeded = false,
                    Message = "No user with this Id"
                    ,
                    StatusCode = 404
                });

            // Act
            var result = await _controller.GetById(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = notFoundResult.Value as dynamic;

            Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public async Task GetByEmail_ValidEmail_ReturnsOkResult()
        {
            // Arrange
            var email = "test@example.com";
            var expectedProfile = new ProfileReadDto { Id = "id", Email = "test@example.com", UserName = "name" };

            _mockProfileManager.Setup(m => m.GetByEmailAsync(email))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = true, Model = expectedProfile });

            // Act
            var result = await _controller.GetByEmail(email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task GetByEmail_InvalidEmail_ReturnsNotFound()
        {
            // Arrange
            var email = "invalid@example.com";

            _mockProfileManager.Setup(m => m.GetByEmailAsync(email))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = false, Message = "No user with this Email", StatusCode = 404 });

            // Act
            var result = await _controller.GetByEmail(email);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = notFoundResult.Value as dynamic;

            Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public async Task Update_ValidDto_ReturnsOkResult()
        {
            // Arrange
            var dto = new ProfileUpdateDto { Email = "test@example.com", UserName = "mynewname", NickName = "anonn" };
            var updatedProfile = new ProfileReadDto { Email = "test@example.com", UserName = "mynewname", NickName = "anonn", ProfilePicture = "a.jpg" };

            _mockProfileManager.Setup(m => m.UpdateAsync(dto))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = true, Model = updatedProfile });

            // Act
            var result = await _controller.Update(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task Update_InvalidDto_ReturnsBadRequest()
        {
            // Arrange
            var dto = new ProfileUpdateDto();

            _controller.ModelState.AddModelError("Email", "Email is required.");

            // Act
            var result = await _controller.Update(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUserLevel_ValidId_ReturnsOkResult()
        {
            // Arrange
            var userId = "validUserId";
            var levelId = 1;

            _mockProfileManager.Setup(m => m.UpdateUserLevelAsync(userId, levelId))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = true, Model = new ProfileReadDto() });

            // Act
            var result = await _controller.UpdateUserLevel(userId, levelId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUserLevel_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var userId = "invalidUserId";
            var levelId = 1;

            _mockProfileManager.Setup(m => m.UpdateUserLevelAsync(userId, levelId))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = false, Message = "No user with this Id", StatusCode = 404 });

            // Act
            var result = await _controller.UpdateUserLevel(userId, levelId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public async Task Delete_ValidEmail_ReturnsOkResult()
        {
            // Arrange
            var email = "testt@gmail.com";

            _mockProfileManager.Setup(m => m.DeleteAsync(email))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = true, Message = "User deleted successfully", StatusCode = 200 });

            // Act
            var result = await _controller.Delete(email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task Delete_InvalidEmail_ReturnsNotFound()
        {
            // Arrange
            var email = "invalid@example.com";

            _mockProfileManager.Setup(m => m.DeleteAsync(email))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = false, Message = "No user with this email", StatusCode = 404 });

            // Act
            var result = await _controller.Delete(email);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<NotFoundObjectResult>(result);

        }
    }
}

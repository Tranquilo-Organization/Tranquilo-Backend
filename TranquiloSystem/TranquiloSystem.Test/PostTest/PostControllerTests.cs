using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using Tranquilo.DAL.Repositories.PostRepo;
using TranquiloSystem.API.Controllers;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.PostDto;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;
using TranquiloSystem.BLL.Manager.PostManager;
using Assert = Xunit.Assert;

//--------------done---------------------
namespace TranquiloSystem.Test.PostTest
{
    //[TestFixture]
    public class PostControllerTests
    {
        private readonly Mock<IPostManager> _mockPostManager;
        private readonly PostController _controller;

        public PostControllerTests()
        {
            _mockPostManager = new Mock<IPostManager>();
            _controller = new PostController(_mockPostManager.Object);
        }

        [Fact]
        public async Task AddPost_WithEmptyPostText_ShouldReturnBadRequest()
        {
            // Arrange
            var postAddDto = new PostAddDto { UserEmail = "moustafaahmednafea@gmail.com", PostText = "" };
            _mockPostManager.Setup(m => m.AddAsync(postAddDto)).ReturnsAsync(new GeneralResponseDto { IsSucceeded = false });

            // Act
            var result = await _controller.AddPost(postAddDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AddPost_WithValidData_ShouldReturnOk()
        {
            // Arrange
            var postAddDto = new PostAddDto { UserEmail = "moustafaahmednafea@gmail.com", PostText = "new post" };
            _mockPostManager.Setup(m => m.AddAsync(postAddDto))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = true });

            // Act
            var result = await _controller.AddPost(postAddDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task GetAllPosts_ReturnsSuccess_WithValidData()
        {
            // Arrange
            var expectedPosts = new List<PostReadDto>();
            _mockPostManager.Setup(m => m.GetAllAsync()).ReturnsAsync(new GeneralResponseDto
            {
                IsSucceeded = true
            });

            // Act
            var result = await _controller.GetAllPosts();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var postId = 1;

            _mockPostManager.Setup(m => m.GetByIdAsync(postId))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = true });

            // Act
            var result = await _controller.GetById(postId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidPostId = 999;

            _mockPostManager.Setup(m => m.GetByIdAsync(invalidPostId))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = false });

            // Act
            var result = await _controller.GetById(invalidPostId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdatePost_ValidData_ReturnsOkResult()
        {
            // Arrange
            var postUpdateDto = new PostUpdateDto { Id = 1, PostText = "updated" };
            var responseDto = new GeneralResponseDto { IsSucceeded = true };

            _mockPostManager.Setup(m => m.UpdateAsync(postUpdateDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.UpdatePost(postUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdatePost_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var postUpdateDto = new PostUpdateDto { PostText = "updated", Id = 2000 };
            var responseDto = new GeneralResponseDto { IsSucceeded = false };

            _mockPostManager.Setup(m => m.UpdateAsync(postUpdateDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.UpdatePost(postUpdateDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeletePost_ValidId_ReturnsOkResult()
        {
            // Arrange
            var postId = 1;
            var responseDto = new GeneralResponseDto { IsSucceeded = true };

            _mockPostManager.Setup(m => m.DeleteAsync(postId)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.Delete(postId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeletePost_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var postId = 200;
            var responseDto = new GeneralResponseDto { IsSucceeded = false };

            _mockPostManager.Setup(m => m.DeleteAsync(postId)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.Delete(postId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}

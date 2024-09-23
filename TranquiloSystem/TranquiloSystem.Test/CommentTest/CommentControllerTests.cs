using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.API.Controllers;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.PostCommentDto;
using TranquiloSystem.BLL.Manager.PostCommentManager;

//--------------------------------doneeeeee---------------------------------

namespace TranquiloSystem.Test.CommentTest
{
    public class CommentControllerTests
    {
        private Mock<IPostCommentManager> _mockManager;
        private CommentController _controller;

        public CommentControllerTests()
        {
            _mockManager = new Mock<IPostCommentManager>();
            _controller = new CommentController(_mockManager.Object);
        }

        [Fact]
        public async Task AddComment_WithInValidPostId_ShouldReturnBadRequest()
        {
            // Arrange 

            var comment = new PostCommentAddDto { CommentText = "newww", UserEmail = "moustafaahmednafea@gmail.com", PostID = 7000 };
            _mockManager.Setup(m => m.AddAsync(It.IsAny<PostCommentAddDto>())).ReturnsAsync(new GeneralResponseDto { IsSucceeded = false });

            // Act
            var result = await _controller.AddComment(comment);

            // Assert 
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task AddComment_WithEmptyCommentText_ShouldReturnBadRequest()
        {
            // Arrange 

            var comment = new PostCommentAddDto { CommentText = "", UserEmail = "valid@email.com", PostID = 7 };
            _mockManager.Setup(m => m.AddAsync(It.IsAny<PostCommentAddDto>())).ReturnsAsync(new GeneralResponseDto { IsSucceeded = false });

            // Act
            var result = await _controller.AddComment(comment);

            // Assert 
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task AddComment_WithValidData_ShouldReturnOk()
        {
            // Arrange 
            var comment = new PostCommentAddDto { CommentText = "new comment", UserEmail = "moustafaahmednafea@gmail.com", PostID = 7 };
            _mockManager.Setup(m => m.AddAsync(It.IsAny<PostCommentAddDto>())).ReturnsAsync(new GeneralResponseDto { IsSucceeded = true });

            // Act
            var result = await _controller.AddComment(comment);

            // Assert 
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllComments_ReturnsSuccess_WithValidData()
        {
            // Arrange
            var expectedComments = new List<PostCommentReadDto>();
            _mockManager.Setup(m => m.GetAllAsync()).Returns(Task.FromResult(new GeneralResponseDto
            {
                IsSucceeded = true,
                Model = expectedComments
            }));

            // Act
            var result = await _controller.GetAllComment();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCommentById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var commentId = 2;


            _mockManager.Setup(s => s.GetByIdAsync(commentId))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = true });

            // Act
            var result = await _controller.GetById(commentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCommentById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidCommentId = 999;

            _mockManager.Setup(m => m.GetByIdAsync(invalidCommentId))
                .ReturnsAsync(new GeneralResponseDto { IsSucceeded = false });

            // Act
            var result = await _controller.GetById(invalidCommentId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetCommentByPostId_ValidPostId_ReturnsOkResult()
        {
            // Arrange
            var postId = 7;
            var comments = new List<PostCommentReadDto>
            {
                new PostCommentReadDto { CommentText = "Comment 1", UserName = "mahmoud", PostID = postId },
                new PostCommentReadDto { CommentText = "Comment 2", UserName = "moustafa", PostID = postId }
            };

            var responseDto = new GeneralResponseDto
            {
                IsSucceeded = true,
                Model = comments,
                StatusCode = 200
            };

            _mockManager.Setup(s => s.GetByPostIdAsync(postId))
                .ReturnsAsync(responseDto);

            // Act
            var result = await _controller.GetByPostId(postId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as GeneralResponseDto;

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetCommentByPostId_InValidPostId_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidPostId = 100;

            _mockManager.Setup(s => s.GetByPostIdAsync(invalidPostId))
                .ReturnsAsync(new GeneralResponseDto
                {
                    StatusCode = 400
                });

            // Act
            var result = await _controller.GetByPostId(invalidPostId);

            // Assert

            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task DeleteComment_ValidId_ReturnsOkResult()
        {
            // Arrange
            var commentId = 1;
            var responseDto = new GeneralResponseDto
            {
                IsSucceeded = true,
            };

            _mockManager.Setup(s => s.DeleteAsync(commentId))
                .ReturnsAsync(responseDto);

            // Act
            var result = await _controller.Delete(commentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task DeleteComment_InvalidId_ReturnsNotFound()
        {
            var commentId = 200;
            var responseDto = new GeneralResponseDto
            {
                IsSucceeded = false,
            };

            _mockManager.Setup(s => s.DeleteAsync(commentId))
                .ReturnsAsync(responseDto);

            // Act
            var result = await _controller.Delete(commentId);

            // Assert

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateComment_ValidData_ReturnsOkResult()
        {
            // Arrange
            var postCommentUpdateDto = new PostCommentUpdateDto
            {
                Id = 1,
                CommentText = "Updated comment"
            };

            var existingComment = new PostComment
            {
                Id = 1,
                CommentText = "Old comment"
            };

            var responseDto = new GeneralResponseDto
            {
                IsSucceeded = true,
            };

            _mockManager.Setup(s => s.UpdateAsync(postCommentUpdateDto))
                .ReturnsAsync(responseDto);

            // Act
            var result = await _controller.UpdateComment(postCommentUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;
            Assert.IsType<OkObjectResult>(result);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateComment_FailedUpdate_ReturnsServerError()
        {
            // Arrange
            var postCommentUpdateDto = new PostCommentUpdateDto
            {
                //no post id 
                CommentText = "Updated comment"
            };

            var responseDto = new GeneralResponseDto
            {
                IsSucceeded = false,
            };

            _mockManager.Setup(s => s.UpdateAsync(postCommentUpdateDto))
                .ReturnsAsync(responseDto);

            // Act
            var result = await _controller.UpdateComment(postCommentUpdateDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
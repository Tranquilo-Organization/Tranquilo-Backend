using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.API.Controllers;
using TranquiloSystem.BLL.Dtos.AccountDto;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Manager.AccountManager;

namespace TranquiloSystem.Test
{
	public class AccountControllerTests
	{
		private readonly Mock<IAccountManager> _mockAccountManager;
		private readonly AccountController _controller;

		public AccountControllerTests()
		{
			_mockAccountManager = new Mock<IAccountManager>();
			_controller = new AccountController(_mockAccountManager.Object);
		}

		[Fact]
		public async Task Register_ValidInput_ReturnsOkResult()
		{
			// Arrange
			var registerDto = new RegisterDto
			{
				UserName = "testtuser",
				Email = "testttttt@gmail.com",
				Password = "Password123a",
				ConfirmPassword = "Password123!a"
			};

			_mockAccountManager.Setup(m => m.Register(registerDto))
				.ReturnsAsync(new GeneralAccountResponse { IsSucceeded = true, Token = "token", Email = registerDto.Email });

			// Act
			var result = await _controller.Register(registerDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var response = okResult.Value as dynamic;
			Assert.Equal("token", response.Token);
			Assert.Equal(200, response.StatusCode);
		}

		[Fact]
		public async Task Register_InvalidInput_ReturnsBadRequest()
		{
			// Arrange
			var registerDto = new RegisterDto
			{
				UserName = "te",
				Email = "invalid-email",
				Password = "123",
				ConfirmPassword = "1234"
			};

			_controller.ModelState.AddModelError("UserName", "Minimum length is 3.");
			_controller.ModelState.AddModelError("Email", "Invalid email.");
			_controller.ModelState.AddModelError("Password", "Minimum length is 4.");
			_controller.ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");

			// Act
			var result = await _controller.Register(registerDto);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal(400, badRequestResult.StatusCode);
		}

		[Fact]
		public async Task Login_ValidInput_ReturnsOkResult()
		{
			// Arrange
			var loginDto = new LoginDto
			{
				Email = "moustafaahmednafea@gmail.com",
				Password = "123456"
			};

			_mockAccountManager.Setup(m => m.Login(loginDto))
				.ReturnsAsync(new GeneralAccountResponse { IsSucceeded = true, Token = "token", Email = loginDto.Email });

			// Act
			var result = await _controller.Login(loginDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var response = okResult.Value as dynamic;
			Assert.Equal("token", response.Token);
			Assert.Equal(200, response.StatusCode);
		}

		[Fact]
		public async Task Login_InvalidInput_ReturnsUnauthorized()
		{
			// Arrange
			var loginDto = new LoginDto
			{
				Email = "test@example.com",
				Password = "O1231564asdasWdA$"
			};

			_mockAccountManager.Setup(m => m.Login(loginDto))
				.ReturnsAsync(new GeneralAccountResponse { IsSucceeded = false, Message = "Email or Password Is Incorrect" });

			// Act
			var result = await _controller.Login(loginDto);

			// Assert
			var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
			var response = unauthorizedResult.Value as dynamic;
			Assert.Equal("Email or Password Is Incorrect", response.Message);
			Assert.Equal(401, response.StatusCode);
		}

	}
}

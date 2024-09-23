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
using TranquiloSystem.BLL.Dtos.OtpDto.OtpDto;
using TranquiloSystem.BLL.Dtos.OtpDto;
using TranquiloSystem.BLL.Manager.AccountManager;

namespace TranquiloSystem.Test.AccountTest
{
    //--------------------done----------------
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
        public async Task Register_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var registerRequestDto = new RegisterDto
            {
                Email = "mmm@gmail.com",
                UserName = "moustafaaaaaaaaa"
            ,
                ConfirmPassword = "Password123",
                Password = "Password123"
            };

            _mockAccountManager.Setup(m => m.Register(registerRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = true, Message = "Registration successful." });

            // Act
            var result = await _controller.Register(registerRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public async Task Register_ExistingEmail_ReturnsBadRequest()
        {
            // Arrange
            var registerRequestDto = new RegisterDto { Email = "test@example.com", UserName = "mssssmmm", Password = "Password123", ConfirmPassword = "Password123" };

            _mockAccountManager.Setup(m => m.Register(registerRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = false, Message = "Email already exists." });

            // Act
            var result = await _controller.Register(registerRequestDto);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = badRequestResult.Value as dynamic;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginRequestDto = new LoginDto { Email = "moustafaahmednafea@gmail.com", Password = "Password123" };

            _mockAccountManager.Setup(m => m.Login(loginRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = true, Token = "token" });

            // Act
            var result = await _controller.Login(loginRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        //this is the same if email or password invalid//
        public async Task Login_InvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            var loginRequestDto = new LoginDto { Email = "invalid@example.com", Password = "wrongpassword" };

            _mockAccountManager.Setup(m => m.Login(loginRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = false, Message = "Email or Password Is Incorrect" });

            // Act
            var result = await _controller.Login(loginRequestDto);

            // Assert
            var UnauthorizedObjectResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var response = UnauthorizedObjectResult.Value as dynamic;
            Assert.IsType<UnauthorizedObjectResult>(result);

        }

        [Fact]
        public async Task ForgotPassword_ValidEmail_ReturnsOkResult()
        {
            // Arrange
            var sendOtpRequestDto = new SendOtpRequestDto { Email = "moustafaahmednafea@gmail.com" };

            _mockAccountManager.Setup(m => m.SendOtpForPasswordReset(sendOtpRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = true, Message = "OTP sent successfully." });

            // Act
            var result = await _controller.ForgotPassword(sendOtpRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task ForgotPassword_InvalidEmail_ReturnsBadRequest()
        {
            // Arrange
            var sendOtpRequestDto = new SendOtpRequestDto { Email = "invalid@example.com" };

            _mockAccountManager.Setup(m => m.SendOtpForPasswordReset(sendOtpRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = false, Message = "User not found." });

            // Act
            var result = await _controller.ForgotPassword(sendOtpRequestDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = badRequestResult.Value as dynamic;
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task VerifyOtp_ValidOtp_ReturnsOkResult()
        {
            // Arrange
            var verifyOtpRequestDto = new VerifyOtpRequestDto { Email = "moustafaahmednafea@gmail.com", Otp = "123456" };

            _mockAccountManager.Setup(m => m.VerifyOtp(verifyOtpRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = true, Message = "OTP verified successfully." });

            // Act
            var result = await _controller.VerifyOtp(verifyOtpRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task VerifyOtp_InvalidOtp_ReturnsBadRequest()
        {
            // Arrange
            var verifyOtpRequestDto = new VerifyOtpRequestDto { Email = "moustafaahmednafea@gmail.com", Otp = "44444" };

            _mockAccountManager.Setup(m => m.VerifyOtp(verifyOtpRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = false, Message = "Invalid or expired OTP." });

            // Act
            var result = await _controller.VerifyOtp(verifyOtpRequestDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = badRequestResult.Value as dynamic;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ResetPasswordWithOtp_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var resetPasswordRequestDto = new ResetPasswordRequestDto { Email = "moustafaahmednafea@gmail.com", Password = "NewPassword123", ConfirmPassword = "NewPassword123", Otp = "123456" };

            _mockAccountManager.Setup(m => m.ResetPasswordWithOtp(resetPasswordRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = true, Message = "Password reset successfully." });

            // Act
            var result = await _controller.ResetPasswordWithOtp(resetPasswordRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;


            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task ResetPasswordWithOtp_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var resetPasswordRequestDto = new ResetPasswordRequestDto { Email = "moustafaahmednafea@gmail.com", Password = "Password123", ConfirmPassword = "Password123568" };

            _mockAccountManager.Setup(m => m.ResetPasswordWithOtp(resetPasswordRequestDto))
                .ReturnsAsync(new GeneralAccountResponse { IsSucceeded = false, Message = "Password and Confirm Password don't match" });

            // Act
            var result = await _controller.ResetPasswordWithOtp(resetPasswordRequestDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = badRequestResult.Value as dynamic;

            Assert.IsType<BadRequestObjectResult>(result);

        }
    }
}

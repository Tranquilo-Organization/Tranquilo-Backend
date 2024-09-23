using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Xunit;
using TranquiloSystem.BLL.Manager.AccountManager;
using TranquiloSystem.BLL.Dtos.AccountDto;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.DAL.Data.Models;
using Tranquilo.DAL.Data.Models;
using Microsoft.Extensions.Configuration;
using TranquiloSystem.BLL.Manager.OtpManager;
using Microsoft.Extensions.Caching.Memory;
using TranquiloSystem.BLL.Manager.EmailManager;
using TranquiloSystem.BLL.Dtos.OtpDto;
using TranquiloSystem.BLL.Dtos.OtpDto.OtpDto;

namespace TranquiloSystem.Test.AccountTest
{
    public class AccountManagerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IOtpManager> _otpManagerMock;
        private readonly Mock<IMemoryCache> _cacheMock;
        private readonly Mock<IEmailManager> _emailManagerMock;
        private readonly AccountManager _accountManager;

        public AccountManagerTests()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();

            _userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object,
            null, null, null, null, null, null, null, null);
            _configurationMock = new Mock<IConfiguration>();
            _otpManagerMock = new Mock<IOtpManager>();
            _cacheMock = new Mock<IMemoryCache>();
            _emailManagerMock = new Mock<IEmailManager>();

            _accountManager = new AccountManager(
                _userManagerMock.Object,
                _configurationMock.Object,
                _otpManagerMock.Object,
                _cacheMock.Object,
                _emailManagerMock.Object
            );
        }


        [Fact]
        public async Task Login_ShouldReturnError_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "moustafaahmednafea@gmail.com", Password = "1111111mois" };
            var user = new ApplicationUser { Email = loginDto.Email, UserName = "moustafa" };
            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginDto.Password)).ReturnsAsync(false);

            // Act
            var result = await _accountManager.Login(loginDto);

            // Assert
            Assert.False(result.IsSucceeded);
            Assert.Equal("Email or Password Is Incorrect", result.Message);
        }
        [Fact]
        public async Task Login_ShouldReturnError_WhenEmailDoesNotExist()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "invalidddddd@gmail.com", Password = "123456" };
            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _accountManager.Login(loginDto);

            // Assert
            Assert.False(result.IsSucceeded);
            Assert.Equal("Email or Password Is Incorrect", result.Message);
        }

        [Fact]
        public async Task Register_ShouldReturnError_WhenEmailAlreadyExists()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "moustafaahmednafea@gmail.com", UserName = "moustafa aahmed", Password = "123456", ConfirmPassword = "123456" };
            _userManagerMock.Setup(um => um.FindByEmailAsync(registerDto.Email)).ReturnsAsync(new ApplicationUser());

            // Act
            var result = await _accountManager.Register(registerDto);

            // Assert
            Assert.False(result.IsSucceeded);
            Assert.Equal("The Email Address Is Already Exists", result.Message);
        }

        [Fact]
        public async Task Register_ShouldReturnError_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "mmmmmmm@gmail.com", UserName = "moustafa", Password = "MOusas123456", ConfirmPassword = "MNIIiasixnx123456" };

            // Act
            var result = await _accountManager.Register(registerDto);

            // Assert
            Assert.False(result.IsSucceeded);
            Assert.Equal("ConfirmPassword and Password do not match.", result.Message);
        }
    }
}

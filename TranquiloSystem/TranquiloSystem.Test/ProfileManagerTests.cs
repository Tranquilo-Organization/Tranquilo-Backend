using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using Tranquilo.DAL.Repositories.AccountRepo;
using TranquiloSystem.BLL.Dtos.ProfileDto;
using TranquiloSystem.BLL.Manager.ProfileManager;

namespace TranquiloSystem.Test
{
	public class ProfileManagerTests
	{
		private readonly ProfileManager _profileManager;
		private readonly Mock<IProfileRepository> _profileRepoMock = new Mock<IProfileRepository>();
		private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
		private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;

		public ProfileManagerTests()
		{
			_userManagerMock = new Mock<UserManager<ApplicationUser>>(
				Mock.Of<IUserStore<ApplicationUser>>(),
				null, null, null, null, null, null, null, null);

			_profileManager = new ProfileManager(_userManagerMock.Object, _mapperMock.Object, _profileRepoMock.Object);
		}

		

		[Fact]
		public async Task GetByIdAsync_ReturnsUser_WhenUserExists()
		{
			// Arrange
			var mockUser = new ApplicationUser { Id = "1", UserName = "User1" };

			_profileRepoMock.Setup(repo => repo.GetByIdAsync("1"))
				.ReturnsAsync(mockUser);

			var mappedUser = new ProfileReadDto { Id = "1", UserName = "User1" };

			_mapperMock.Setup(mapper => mapper.Map<ProfileReadDto>(mockUser))
				.Returns(mappedUser);

			// Act
			var result = await _profileManager.GetUsersByIdAsync("1");

			// Assert
			Assert.True(result.IsSucceeded);
			Assert.Equal(200, result.StatusCode);
			Assert.Equal(mappedUser, result.Model);
		}

		[Fact]
		public async Task GetByIdAsync_ReturnsNotFound_WhenUserDoesNotExist()
		{
			// Arrange
			_profileRepoMock.Setup(repo => repo.GetByIdAsync("1"))
				.ReturnsAsync((ApplicationUser)null);

			// Act
			var result = await _profileManager.GetUsersByIdAsync("1");

			// Assert
			Assert.False(result.IsSucceeded);
			Assert.Equal(404, result.StatusCode);
			Assert.Equal("No user with this Id", result.Message);
		}
		[Fact]
		public async Task GetByEmailAsync_ReturnsSuccess_WhenUserExists()
		{
			// Arrange
			var mockUser = new ApplicationUser { Email = "moustafaahmednafea@gmail.com" };
			_profileRepoMock.Setup(repo => repo.GetByEmailAsync(mockUser.Email))
				.ReturnsAsync(mockUser); 

			// Act
			var result = await _profileManager.GetByEmailAsync(mockUser.Email);

			// Assert
			Assert.True(result.IsSucceeded);
			Assert.Equal(200, result.StatusCode);
			Assert.NotNull(result.Model);
		}
		[Fact]
		public async Task GetByEmailAsync_ReturnsNotFound_WhenUserDoesNotExist()
		{
			// Arrange
			var email = "nonexistent@example.com"; 
			_profileRepoMock.Setup(repo => repo.GetByEmailAsync(email))
				.ReturnsAsync((ApplicationUser)null); 

			// Act
			var result = await _profileManager.GetByEmailAsync(email);

			// Assert
			Assert.False(result.IsSucceeded);
			Assert.Equal(404, result.StatusCode);
			Assert.Equal("No user with this Email", result.Message);
		}

		[Fact]
		public async Task UpdateAsync_ReturnsNotFound_WhenUserDoesNotExist()
		{
			// Arrange
			var updateDto = new ProfileUpdateDto { Email = "invalid@gmail.com", UserName = "UpdatedUser" };

			_profileRepoMock.Setup(repo => repo.GetByEmailAsync("invalid@gmail.com"))
				.ReturnsAsync((ApplicationUser)null);

			// Act
			var result = await _profileManager.UpdateAsync(updateDto);

			// Assert
			Assert.False(result.IsSucceeded);
			Assert.Equal(404, result.StatusCode);
			Assert.Equal("No user with this Email", result.Message);
		}
		[Fact]
		public async Task UpdateAsync_ReturnsUpdatedUser_WhenSuccessful()
		{
			// Arrange
			var mockUser = new ApplicationUser { Email = "hima@gmail.com", UserName = "Hima" };
			var updateDto = new ProfileUpdateDto { Email = "newhima@gmail.com", UserName = "UpdatedUser" };

			_profileRepoMock.Setup(repo => repo.GetByEmailAsync("hima@gmail.com"))
				.ReturnsAsync(mockUser);

			mockUser.Email = updateDto.Email;
			mockUser.UserName = updateDto.UserName;

			_profileRepoMock.Setup(repo => repo.UpdateAsync(mockUser))
				.ReturnsAsync(mockUser);

			var updatedUser = new ProfileReadDto { Id = mockUser.Id, Email = updateDto.Email, UserName = updateDto.UserName };

			_mapperMock.Setup(mapper => mapper.Map<ProfileReadDto>(mockUser))
				.Returns(updatedUser);

			// Act
			var result = await _profileManager.UpdateAsync(updateDto);

			// Assert
			Assert.True(result.IsSucceeded);
			Assert.Equal(200, result.StatusCode);
			Assert.Equal(updatedUser, result.Model);
		}

		[Fact]
		public async Task DeleteAsync_ReturnsSuccess_WhenUserDeleted()
		{
			// Arrange
			var mockUser = new ApplicationUser { Id = "1", UserName = "User1" };

			_profileRepoMock.Setup(repo => repo.GetByIdAsync("1"))
				.ReturnsAsync(mockUser);

			_profileRepoMock.Setup(repo => repo.DeleteAsync(mockUser))
				.ReturnsAsync(true);

			// Act
			var result = await _profileManager.DeleteAsync("75a84bde-0694-40bb-a618-2c011e5eb728");

			// Assert
			Assert.True(result.IsSucceeded);
			Assert.Equal(200, result.StatusCode);
			Assert.Equal("User deleted successfully", result.Message);
		}

		[Fact]
		public async Task DeleteAsync_ReturnsNotFound_WhenUserNotExists()
		{
			// Arrange
			_profileRepoMock.Setup(repo => repo.GetByIdAsync("1"))
				.ReturnsAsync((ApplicationUser)null);

			// Act
			var result = await _profileManager.DeleteAsync("1");

			// Assert
			Assert.False(result.IsSucceeded);
			Assert.Equal(404, result.StatusCode);
			Assert.Equal("No user with this Id", result.Message);
		}
	}
}

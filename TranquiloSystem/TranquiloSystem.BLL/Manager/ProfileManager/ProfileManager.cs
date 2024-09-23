using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using Tranquilo.DAL.Repositories.AccountRepo;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.ProfileDto;

namespace TranquiloSystem.BLL.Manager.ProfileManager
{
	public class ProfileManager : IProfileManager
	{
		private readonly UserManager<ApplicationUser> _userManager;
	    private readonly IMapper _mapper;
		private readonly IProfileRepository _profileRepository;

		public ProfileManager(UserManager<ApplicationUser> userManager, IMapper mapper, IProfileRepository profileRepository)
		{
			_userManager = userManager;
			_mapper = mapper;
			_profileRepository = profileRepository;
		}

		public async Task<GeneralResponseDto> GetByEmailAsync(string email)
		{
			var user = await _profileRepository.GetByEmailAsync(email);
			if(user == null)
			{
				return new GeneralResponseDto
				{
					Message = "No user with this Email",
					IsSucceeded = false,
					StatusCode = 404
				};
			}
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				StatusCode = 200,
				Model = _mapper.Map<ProfileReadDto>(user)
			};
		}
		public async Task<GeneralResponseDto> GetUsersByIdAsync(string id)
		{
			var user = await _profileRepository.GetByIdAsync(id);
			if (user == null)
			{
				return new GeneralResponseDto
				{
					Message = "No user with this Id",
					IsSucceeded = false,
					StatusCode = 404
				};
			}
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				StatusCode = 200,
				Model = _mapper.Map<ProfileReadDto>(user)
			};
		}
		public async Task<GeneralResponseDto> UpdateAsync(ProfileUpdateDto profileUpdateDto)
		{
			var existingUser = await _profileRepository.GetByEmailAsync(profileUpdateDto.Email);
			if (existingUser == null)
			{
				return new GeneralResponseDto
				{
					Message = "No user with this Email",
					IsSucceeded = false,
					StatusCode = 404
				};
			}

			
			if (!string.IsNullOrWhiteSpace(profileUpdateDto.ProfilePicture))
			{
				existingUser.ProfilePicture = profileUpdateDto.ProfilePicture;
			}

			if (!string.IsNullOrWhiteSpace(profileUpdateDto.UserName))
			{
				existingUser.UserName = profileUpdateDto.UserName;
			}

			if (!string.IsNullOrWhiteSpace(profileUpdateDto.NickName))
			{
				existingUser.NickName = profileUpdateDto.NickName;
			}

			if (!string.IsNullOrWhiteSpace(profileUpdateDto.Email) && profileUpdateDto.Email != existingUser.Email)
			{
				existingUser.Email = profileUpdateDto.Email;
			}

			//_mapper.Map(profileUpdateDto, existingUser);
			var updatedProfile = await _profileRepository.UpdateAsync(existingUser);

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				StatusCode = 200,
				Model = _mapper.Map<ProfileReadDto>(updatedProfile)
			};
		}

		public async Task<GeneralResponseDto> UpdateUserLevelAsync(string UserId, int levelId)
		{
			var user = await _profileRepository.GetByIdAsync(UserId);
			if (user == null)
			{
				return new GeneralResponseDto
				{
					Message = "No user with this Id",
					IsSucceeded = false,
					StatusCode = 404
				};
			}
			
			var updatedUser = await _profileRepository.UpdateUserLevelAsync(user,levelId);
			if(updatedUser == null)
			{
				return new GeneralResponseDto
				{
					Message = "No level with this Id",
					IsSucceeded = false,
					StatusCode = 404
				};
			}

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				StatusCode = 200,
				Model = _mapper.Map<ProfileReadDto>(updatedUser)
			};

		}

		public async Task<GeneralResponseDto> DeleteAsync(string email)
		{
			var user = await _profileRepository.GetByEmailAsync(email);
			if (user == null)
			{
				return new GeneralResponseDto
				{
					Message = "No user with this email",
					IsSucceeded = false,
					StatusCode = 404
				};
			}

			var IsDeleted= await _profileRepository.DeleteAsync(user);
			if (IsDeleted)
			{
				return new GeneralResponseDto
				{
					Message = "User deleted successfully",
					IsSucceeded = true, 
					StatusCode = 200
				};
			}
			else
				return new GeneralResponseDto
				{
					Message = "An error occurred during deletion",
					IsSucceeded = false,
					StatusCode = 500
				};
		}

		
	}
}

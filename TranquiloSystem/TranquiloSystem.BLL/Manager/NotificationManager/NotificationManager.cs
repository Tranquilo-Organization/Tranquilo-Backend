using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.DAL.Repositories.NotificarioRepo;
using TranquiloSystem.DAL.Data.Models;
using Tranquilo.DAL.Repositories.AccountRepo;
using Microsoft.AspNetCore.Identity;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using AutoMapper;
using TranquiloSystem.BLL.Dtos.NotificationDto;

namespace TranquiloSystem.BLL.Manager.Notification
{
    public class NotificationManager : INotificationManager
	{
		private readonly INotificationRepository _notificationRepository;
		private readonly IProfileRepository _profileRepository;
		private readonly IMapper _mapper;
		public NotificationManager(INotificationRepository notificationRepository, IProfileRepository profileRepository, IMapper mapper)
		{
			_notificationRepository = notificationRepository;
			_profileRepository = profileRepository;
			_mapper = mapper;
		}

		public async Task<GeneralResponseDto> AddNotificationAsync(string userId, string message)
		{
			var userExist = await _profileRepository.GetByIdAsync(userId);
			if (userExist == null)
			{
				return new GeneralResponseDto
				{
					Message = "User not found.",
					IsSucceeded = false,
					StatusCode = 404
				};
			}
			var notification = new DAL.Data.Models.Notification
			{
				UserId = userId,
				Message = message,
				CreatedDate = DateTime.UtcNow,
				IsRead = false
			};

			await _notificationRepository.AddAsync(notification);
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				StatusCode = 201,
				Message = "Notification added successfully."
			};
		}

		public async Task<GeneralResponseDto> GetUserNotificationsAsync(string userId)
		{
			var userExist = await _profileRepository.GetByIdAsync(userId);
			if (userExist == null)
			{
				return new GeneralResponseDto
				{
					Message = "User not found.",
					IsSucceeded = false,
					StatusCode = 404
				};
			}
			var notifications = await _notificationRepository.GetByUserIdAsync(userId);
			var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				StatusCode = 200,
				Model = notificationDtos
			};
		}

		public async Task<GeneralResponseDto> MarkNotificationAsReadAsync(int notificationId)
		{
			var notification = await _notificationRepository.GetByIdAsync(notificationId);
			if (notification == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					StatusCode = 404,
					Message = "Notification not found."
				};
			}

			notification.IsRead = true;
			await _notificationRepository.UpdateAsync(notification);

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				StatusCode = 200,
				Message = "Notification marked as read."
			};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.BLL.Dtos;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.DAL.Data.Models;

namespace TranquiloSystem.BLL.Manager.Notification
{
	public interface INotificationManager
	{
		Task<GeneralResponseDto> AddNotificationAsync(string userId, string message);
		Task<GeneralResponseDto> GetUserNotificationsAsync(string userId);
		Task<GeneralResponseDto> MarkNotificationAsReadAsync(int notificationId);
	}
}

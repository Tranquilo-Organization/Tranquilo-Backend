using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.DAL.Data.Models;

namespace TranquiloSystem.DAL.Repositories.NotificarioRepo
{
	public interface INotificationRepository
	{
		Task AddAsync(Notification notification);
		Task<Notification> GetByIdAsync(int id);
		Task<List<Notification>> GetByUserIdAsync(string userId);
		Task UpdateAsync(Notification notification);
	}
}

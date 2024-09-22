using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.DAL.Data.DbHelper;
using TranquiloSystem.DAL.Data.Models;

namespace TranquiloSystem.DAL.Repositories.NotificarioRepo
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly TranquiloContext _context;

		public NotificationRepository(TranquiloContext context)
		{
			_context = context;
		}
		public async Task AddAsync(Notification notification)
		{
			await _context.Notifications.AddAsync(notification);
			await _context.SaveChangesAsync();
		}

		public async Task<Notification> GetByIdAsync(int id)
		{
			return await _context.Notifications.FindAsync(id);
		}

		public async Task<List<Notification>> GetByUserIdAsync(string userId)
		{
			return await _context.Notifications
				.Where(n => n.UserId == userId)
				.ToListAsync();
		}

		public async Task UpdateAsync(Notification notification)
		{
			_context.Notifications.Update(notification);
			await _context.SaveChangesAsync();
		}
	}
}

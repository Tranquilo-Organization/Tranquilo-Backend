using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.DAL.Data.DbHelper;

namespace Tranquilo.DAL.Repositories.AccountRepo
{
    public class ProfileRepository:IProfileRepository
    {
		private readonly TranquiloContext _context;
		public ProfileRepository(TranquiloContext context)
		{
			_context = context;
		}

		public async Task<ApplicationUser> GetByEmailAsync(string email)
		{
			var user = await _context.Users
				.Where(x => x.IsDeleted == false)
				.Include(x => x.Level)
				.FirstOrDefaultAsync(x => x.Email == email);

			if(user == null)
			{
				return null;
			}
			return user;

		}

		public async Task<ApplicationUser> GetByIdAsync(string id)
		{
			var user = await _context.Users
				.Where(x => x.IsDeleted == false)
				.Include(x => x.Level)
				.FirstOrDefaultAsync(x => x.Id == id);
			
			return user ?? null;
		}
	
		public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return user;
		}

		public async Task<ApplicationUser> UpdateUserLevelAsync(ApplicationUser user, int levelId)
		{		
			var levelExists =await _context.Levels
				.AsNoTracking().FirstOrDefaultAsync(x => x.Id == levelId);
			if(levelExists == null)
			{
				return null;
			}

			user.LevelId = levelId;
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return user;
		}
		public async Task<bool> DeleteAsync(ApplicationUser user)
		{
			_context.Users.Remove(user);
			var result =  await _context.SaveChangesAsync();
			return result > 0;
		}
	}
}
 
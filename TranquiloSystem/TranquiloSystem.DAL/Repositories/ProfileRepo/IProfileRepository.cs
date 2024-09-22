using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace Tranquilo.DAL.Repositories.AccountRepo
{
    public interface IProfileRepository
    {
		Task<ApplicationUser> GetByIdAsync(string id);
		Task<ApplicationUser> GetByEmailAsync(string email);
		Task<ApplicationUser> UpdateAsync(ApplicationUser user);
		Task<ApplicationUser> UpdateUserLevelAsync(ApplicationUser user, int levelId);
		Task<bool> DeleteAsync(ApplicationUser user);
	}
}

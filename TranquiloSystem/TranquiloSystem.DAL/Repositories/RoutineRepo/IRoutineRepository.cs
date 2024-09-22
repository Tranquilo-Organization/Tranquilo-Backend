using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace Tranquilo.DAL.Repositories.RoutineRepo
{
    public interface IRoutineRepository
    {
		Task<IEnumerable<Routine>> GetAllAsync();
		Task<Routine> GetByIdAsync(int id);
		Task<ICollection<Routine>> GetByLevelIdAsync(int levelId);
	}
}

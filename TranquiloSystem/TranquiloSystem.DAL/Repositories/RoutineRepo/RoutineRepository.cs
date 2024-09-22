using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.DAL.Data.DbHelper;

namespace Tranquilo.DAL.Repositories.RoutineRepo
{
	public class RoutineRepository : IRoutineRepository
	{
		private readonly TranquiloContext _context;

		public RoutineRepository(TranquiloContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Routine>> GetAllAsync()
		{
			var result = await _context.Routines
				.Include(x => x.Level).AsNoTracking().ToListAsync();
			return result;
		}

		public async Task<Routine> GetByIdAsync(int id)
		{
			var routine = await _context.Routines
				.Include(x => x.Level).FirstOrDefaultAsync(x=>x.Id == id);
			if(routine == null)
			{
				return null;
			}
			return routine;
		}
		public async Task<ICollection<Routine>> GetByLevelIdAsync(int levelId)
		{
			var level = await _context.Levels
				.FindAsync(levelId);
			if(level == null)
			{
				return null;
			}

			var routines = await _context.Routines
				.Include(x => x.Level)
				.Where(x=>x.LevelId == levelId).ToListAsync();

			if(routines == null)
			{
				return null;
			}
			return routines;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.RoutineDto;

namespace TranquiloSystem.BLL.Manager.RoutineManager
{
	public interface IRoutineManager
	{
		Task<GeneralResponseDto> GetAllAsync();
		Task<GeneralResponseDto> GetByIdAsync(int id);
		Task<GeneralResponseDto> GetByLevelIdAsync(int levelId);
	}
}

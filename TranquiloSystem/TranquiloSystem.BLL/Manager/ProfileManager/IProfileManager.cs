using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.ProfileDto;

namespace TranquiloSystem.BLL.Manager.ProfileManager
{
	public interface IProfileManager
	{
		Task<GeneralResponseDto> GetUsersByIdAsync(string id);
		Task<GeneralResponseDto> GetByEmailAsync(string email);
		Task<GeneralResponseDto> UpdateAsync(ProfileUpdateDto profileUpdateDto);
		Task<GeneralResponseDto> UpdateUserLevelAsync(string UserId,int levelId);
		Task<GeneralResponseDto> DeleteAsync(string id);

	}
}

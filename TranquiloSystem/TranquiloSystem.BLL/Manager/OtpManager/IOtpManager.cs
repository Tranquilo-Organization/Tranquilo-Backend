using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.BLL.Dtos.AccountDto;

namespace TranquiloSystem.BLL.Manager.OtpManager
{
	public interface IOtpManager
	{
		Task<string> GenerateOtpAsync(string email);
		Task RemoveOtpAsync(string email);
	}
}

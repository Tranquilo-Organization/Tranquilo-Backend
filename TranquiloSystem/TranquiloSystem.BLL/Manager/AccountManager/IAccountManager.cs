using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.BLL.Dtos.AccountDto;

namespace TranquiloSystem.BLL.Manager.AccountManager
{
    public interface IAccountManager
    {
		Task<GeneralResponse> Login(LoginDto loginDto);
		Task<GeneralResponse> Register(RegisterDto registerDto);
	}
}

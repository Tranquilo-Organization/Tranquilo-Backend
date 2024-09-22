using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.BLL.Dtos.AccountDto;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.OtpDto;
using TranquiloSystem.BLL.Dtos.OtpDto.OtpDto;

namespace TranquiloSystem.BLL.Manager.AccountManager
{
    public interface IAccountManager
	{
		Task<GeneralAccountResponse> Login(LoginDto loginDto);
		Task<GeneralAccountResponse> Register(RegisterDto registerDto);
		Task<GeneralAccountResponse> SendOtpForPasswordReset(SendOtpRequestDto dto);
		Task<GeneralAccountResponse> VerifyOtp(VerifyOtpRequestDto dto);
		Task<GeneralAccountResponse> ResetPasswordWithOtp(ResetPasswordRequestDto dto);
	}
}
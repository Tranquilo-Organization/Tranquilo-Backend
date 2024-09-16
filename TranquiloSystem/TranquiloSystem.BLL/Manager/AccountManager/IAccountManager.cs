using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.BLL.Dtos.AccountDto;
using TranquiloSystem.BLL.Dtos.OtpDto;
using TranquiloSystem.BLL.Dtos.OtpDto.OtpDto;

namespace TranquiloSystem.BLL.Manager.AccountManager
{
	public interface IAccountManager
	{
		Task<GeneralResponse> Login(LoginDto loginDto);
		Task<GeneralResponse> Register(RegisterDto registerDto);
		Task<GeneralResponse> SendOtpForPasswordReset(SendOtpRequestDto dto);
		Task<GeneralResponse> VerifyOtp(VerifyOtpRequestDto dto);
		Task<GeneralResponse> ResetPasswordWithOtp(ResetPasswordRequestDto dto);
	}
}
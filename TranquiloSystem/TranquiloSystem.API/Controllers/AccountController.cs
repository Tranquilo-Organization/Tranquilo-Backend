using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranquiloSystem.BLL.Dtos.AccountDto;
using TranquiloSystem.BLL.Dtos.OtpDto;
using TranquiloSystem.BLL.Dtos.OtpDto.OtpDto;
using TranquiloSystem.BLL.Manager.AccountManager;
using TranquiloSystem.DAL.Data.Models;

namespace TranquiloSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			if(ModelState.IsValid)
			{
				var result = await _accountManager.Register(registerDto);
				if (result.IsSucceeded == false)
				{
					return BadRequest(result.Message);
				}
				return Ok(new { result.Token, result.ExpireDate });
			}
			return BadRequest();
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var result = await _accountManager.Login(loginDto);
			if(result.IsSucceeded == false)
			{
				return Unauthorized(result.Message);
			}
			return Ok(new {result.Token, result.ExpireDate});
		}

		[HttpPost("forgot-password")]
		public async Task<IActionResult> ForgotPassword([FromBody] SendOtpRequestDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var response = await _accountManager.SendOtpForPasswordReset(dto);
			if (!response.IsSucceeded)
			{
				return BadRequest(response.Message);
			}
			return Ok(response.Message);
		}

		[HttpPost("verify-otp")]
		public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var response = await _accountManager.VerifyOtp(dto);
			if (!response.IsSucceeded)
			{
				return BadRequest(response.Message);
			}
			return Ok(response.Message);
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPasswordWithOtp([FromBody] ResetPasswordRequestDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var response = await _accountManager.ResetPasswordWithOtp(dto);
			if (!response.IsSucceeded)
			{
				return BadRequest(response.Message);
			}

			return Ok(new
			{
				token = response.Token,
				expireDate = response.ExpireDate,
				message = response.Message
			});
		}


    }
}

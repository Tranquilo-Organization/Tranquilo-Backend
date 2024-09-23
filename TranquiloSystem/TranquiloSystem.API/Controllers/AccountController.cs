using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
			if (ModelState.IsValid)
			{
				var result = await _accountManager.Register(registerDto);
				if (result.IsSucceeded == false)
				{
					return BadRequest(new { result.Message , StatusCode = 400 });
				}
				return Ok(new { result.Token, result.ExpireDate, result.Email, result.UserName, result.Id,StatusCode = 200 });
			}
			return BadRequest(new {ModelState, StatusCode = 400 });
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var result = await _accountManager.Login(loginDto);
			if(result.IsSucceeded == false)
			{
				return Unauthorized(new { result.Message, StatusCode = 401 });
			}
			return Ok(new {result.Token, result.ExpireDate, result.Email, result.UserName, result.Id, StatusCode = 200});
		}

		[HttpPost("forgot-password")]
		public async Task<IActionResult> ForgotPassword([FromBody] SendOtpRequestDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { ModelState, StatusCode = 400 });
			}
			var response = await _accountManager.SendOtpForPasswordReset(dto);
			if (!response.IsSucceeded)
			{
				return BadRequest(new { response.Message, StatusCode = 400 });
			}
			return Ok(new { response.Message, statusCode = 200 });
		}

		[HttpPost("verify-otp")]
		public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { ModelState, StatusCode = 400 });
			}

			var response = await _accountManager.VerifyOtp(dto);
			if (!response.IsSucceeded)
			{
				return BadRequest(new { response.Message, StatusCode = 400 });
			}
			return Ok(new { response.Message, statusCode = 200 });
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPasswordWithOtp([FromBody] ResetPasswordRequestDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { ModelState, StatusCode = 400 });
			}

			var response = await _accountManager.ResetPasswordWithOtp(dto);
			if (!response.IsSucceeded)
			{
				return BadRequest(new { response.Message, StatusCode = 400 });
			}

			return Ok(new
			{
				token = response.Token,
				expireDate = response.ExpireDate,
				message = response.Message,
				StatusCode = 200
			});
		}


    }
}

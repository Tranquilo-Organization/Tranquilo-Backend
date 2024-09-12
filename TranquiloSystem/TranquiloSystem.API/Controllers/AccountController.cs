using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranquiloSystem.BLL.Dtos.AccountDto;
using TranquiloSystem.BLL.Manager.AccountManager;

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
				if (result == null)
				{
					return BadRequest();
				}
				return Ok(new { result.Token, result.ExpireDate });
			}
			return BadRequest();
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var result =await _accountManager.Login(loginDto);
			if(result == null)
			{
				return Unauthorized("Email or Password is incorrect");
			}
			return Ok(new {result.Token, result.ExpireDate});
		}


		
    }
}

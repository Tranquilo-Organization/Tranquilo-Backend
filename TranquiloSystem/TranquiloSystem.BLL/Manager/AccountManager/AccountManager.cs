using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.BLL.Dtos.AccountDto;

namespace TranquiloSystem.BLL.Manager.AccountManager
{
	public class AccountManager : IAccountManager
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;

		public AccountManager(UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<GeneralResponse> Login(LoginDto loginDto)
		{
			GeneralResponse generalResponse = new GeneralResponse();
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
				return null;

			var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
			if(!result)
			{
				return null;
			}

			var claims = await _userManager.GetClaimsAsync(user);

			generalResponse = GeneralToken(claims);
			return generalResponse;

		}

		public async Task<GeneralResponse> Register(RegisterDto registerDto)
		{
			GeneralResponse generalResponse = new GeneralResponse();
			ApplicationUser AppUser = new ApplicationUser();

			AppUser.UserName = registerDto.UserName;
			AppUser.Email = registerDto.Email;
			IdentityResult identityResult = await _userManager.CreateAsync(AppUser, registerDto.Password);

			if(identityResult == null)
			{
				return null;
			}
			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
			claims.Add(new Claim(ClaimTypes.Name, AppUser.UserName));
			claims.Add(new Claim(ClaimTypes.Email, AppUser.Email));

			await _userManager.AddClaimsAsync(AppUser, claims);

			generalResponse = GeneralToken(claims);
			return generalResponse;
		}

		private GeneralResponse GeneralToken(IList<Claim> claims)
		{
			
			var securityKeyOfString = _configuration.GetSection("Key").Value;
			var securityKeyOfBytes = Encoding.ASCII.GetBytes(securityKeyOfString);
			var securityKey = new SymmetricSecurityKey(securityKeyOfBytes);

			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
		
			var expireDate = DateTime.Now.AddDays(2);
			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				claims: claims,
				expires: expireDate,
				signingCredentials: signingCredentials
				);

			JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

			return new GeneralResponse
			{
				Token = token,
				ExpireDate = expireDate
			};
		}
	}
}

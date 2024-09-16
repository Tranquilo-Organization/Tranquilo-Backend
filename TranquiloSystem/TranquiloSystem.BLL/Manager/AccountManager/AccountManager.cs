using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.BLL.Dtos.AccountDto;
using TranquiloSystem.BLL.Dtos.OtpDto;
using TranquiloSystem.BLL.Dtos.OtpDto.OtpDto;
using TranquiloSystem.BLL.Manager.EmailManager;
using TranquiloSystem.BLL.Manager.OtpManager;

namespace TranquiloSystem.BLL.Manager.AccountManager
{
	public class AccountManager : IAccountManager
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;
		private readonly IOtpManager _otpManager;
		private readonly IMemoryCache _cache;
		private readonly IEmailManager _emailManager;

		public AccountManager(UserManager<ApplicationUser> userManager, IConfiguration configuration, IOtpManager otpManager, IMemoryCache cache, IEmailManager emailManager)
		{
			_userManager = userManager;
			_configuration = configuration;
			_otpManager = otpManager;
			_cache = cache;
			_emailManager = emailManager;
		}

		public async Task<GeneralResponse> Login(LoginDto loginDto)
		{
			GeneralResponse generalResponse = new GeneralResponse();

			var user = await _userManager.FindByEmailAsync(loginDto.Email);

			if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
			{
				generalResponse.IsSucceeded = false;
				generalResponse.Message = "Email or Password Is Incorrect";
				return generalResponse;
			}
				
			var claims = await _userManager.GetClaimsAsync(user);

			generalResponse = GeneralToken(claims);
			generalResponse.IsSucceeded = true;
			return generalResponse;

		}

		public async Task<GeneralResponse> Register(RegisterDto registerDto)
		{			
			GeneralResponse generalResponse = new GeneralResponse();

			var emailExists = await _userManager.FindByEmailAsync(registerDto.Email);
			if (emailExists != null)
			{
				generalResponse.Message = "The Email Adress Is Already Exists";
				generalResponse.IsSucceeded = false;
				return generalResponse;
			}
			
			
			ApplicationUser AppUser = new ApplicationUser();

			
			AppUser.UserName = registerDto.UserName;
			AppUser.Email = registerDto.Email;
			IdentityResult identityResult = await _userManager.CreateAsync(AppUser, registerDto.Password);

			if(!identityResult.Succeeded)
			{
				generalResponse.Message = string.Join(", ", identityResult.Errors.Select(e => e.Description));
				generalResponse.IsSucceeded = false;
				return generalResponse;
			}
			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
			claims.Add(new Claim(ClaimTypes.Name, AppUser.UserName));
			claims.Add(new Claim(ClaimTypes.Email, AppUser.Email));

			await _userManager.AddClaimsAsync(AppUser, claims);

			generalResponse = GeneralToken(claims);
			generalResponse.IsSucceeded = true;
			return generalResponse;
		}

		public async Task<GeneralResponse> SendOtpForPasswordReset(SendOtpRequestDto dto)
		{
			GeneralResponse generalResponse = new GeneralResponse();

			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null)
			{
				generalResponse.IsSucceeded = false;
				generalResponse.Message = "User not found";
				return generalResponse;
			}

			var otpCode  = await _otpManager.GenerateOtpAsync(dto.Email);


			var emailResponse = await _emailManager.SendEmailAsync(user.Email, "Your Password Reset OTP Code", $"Your OTP code for resetting your password is: {otpCode}");
			if (!emailResponse.IsSucceeded)
			{
				return emailResponse; // Return the email error if sending failed
			}
			
			generalResponse.Message = "OTP sent to email successfully.";
			generalResponse.IsSucceeded = emailResponse.IsSucceeded;
			return generalResponse;
		}


		public async Task<GeneralResponse> VerifyOtp(VerifyOtpRequestDto dto)
		{
			GeneralResponse generalResponse = new GeneralResponse();

			// Retrieve email from cache using OTP
			if (!_cache.TryGetValue($"{dto.Email}_Verified", out string storedOtp) || storedOtp != dto.Otp)
			{
				generalResponse.IsSucceeded = false;
				generalResponse.Message = "Invalid or expired OTP";
				return generalResponse;
			}

			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null)
			{
				generalResponse.IsSucceeded = false;
				generalResponse.Message = "User not found";
				return generalResponse;
			}

			_cache.Set($"{dto.Email}_Verified", true, TimeSpan.FromMinutes(10));

			generalResponse.IsSucceeded = true;
			generalResponse.Message = "OTP verified successfully. You can now reset your password.";
			return generalResponse;
		}

		public async Task<GeneralResponse> ResetPasswordWithOtp(ResetPasswordRequestDto dto)
		{
			GeneralResponse generalResponse = new GeneralResponse();

			if (!_cache.TryGetValue($"{dto.Email}_Verified", out bool otpVerified) || !otpVerified)
			{
				generalResponse.IsSucceeded = false;
				generalResponse.Message = "Invalid or expired OTP";
				return generalResponse;
			}

			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null)
			{
				generalResponse.IsSucceeded = false;
				generalResponse.Message = "User not found";
				return generalResponse;
			}

			var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, resetToken, dto.Password);
			if (!result.Succeeded)
			{
				generalResponse.IsSucceeded = false;
				generalResponse.Message = string.Join(", ", result.Errors.Select(e => e.Description));
				return generalResponse;
			}

			await _otpManager.RemoveOtpAsync(dto.Email);

			var claims = await _userManager.GetClaimsAsync(user);
			generalResponse = GeneralToken(claims);
			generalResponse.IsSucceeded = true;
			generalResponse.Message = "Password reset successfully";
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

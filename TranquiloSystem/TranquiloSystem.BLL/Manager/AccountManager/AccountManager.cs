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
using TranquiloSystem.BLL.Dtos.GeneralDto;
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

		public async Task<GeneralAccountResponse> Login(LoginDto loginDto)
		{
			GeneralAccountResponse GeneralAccountResponse = new GeneralAccountResponse();

			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
			{
				GeneralAccountResponse.IsSucceeded = false;
				GeneralAccountResponse.Message = "Email or Password Is Incorrect";
				return GeneralAccountResponse;
			}
				
			var claims = await _userManager.GetClaimsAsync(user);

			GeneralAccountResponse = GeneralToken(claims);
			GeneralAccountResponse.IsSucceeded = true;
			GeneralAccountResponse.Email = user.Email;
			GeneralAccountResponse.Id = user.Id;
			return GeneralAccountResponse;

		}

		public async Task<GeneralAccountResponse> Register(RegisterDto registerDto)
		{			
			GeneralAccountResponse GeneralAccountResponse = new GeneralAccountResponse();

			if (registerDto.Password != registerDto.ConfirmPassword)
			{
				GeneralAccountResponse.Message = "ConfirmPassword and Password do not match.";
				GeneralAccountResponse.IsSucceeded = false;
				return GeneralAccountResponse;
			}

			var emailExists = await _userManager.FindByEmailAsync(registerDto.Email);
			if (emailExists != null)
			{
				GeneralAccountResponse.Message = "The Email Address Is Already Exists";
				GeneralAccountResponse.IsSucceeded = false;
				return GeneralAccountResponse;
			}
			
			
			ApplicationUser AppUser = new ApplicationUser();

			AppUser.UserName = registerDto.UserName;
			AppUser.Email = registerDto.Email;
			IdentityResult identityResult = await _userManager.CreateAsync(AppUser, registerDto.Password);

			if(!identityResult.Succeeded)
			{
				GeneralAccountResponse.Message = string.Join(", ", identityResult.Errors.Select(e => e.Description));
				GeneralAccountResponse.IsSucceeded = false;
				return GeneralAccountResponse;
			}
			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
			claims.Add(new Claim(ClaimTypes.Name, AppUser.UserName));
			claims.Add(new Claim(ClaimTypes.Email, AppUser.Email));

			await _userManager.AddClaimsAsync(AppUser, claims);

			GeneralAccountResponse = GeneralToken(claims);
			GeneralAccountResponse.IsSucceeded = true;
			GeneralAccountResponse.Email = AppUser.Email;
			GeneralAccountResponse.Id = AppUser.Id;
			return GeneralAccountResponse;
		}

		public async Task<GeneralAccountResponse> SendOtpForPasswordReset(SendOtpRequestDto dto)
		{
			GeneralAccountResponse GeneralAccountResponse = new GeneralAccountResponse();

			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null)
			{
				GeneralAccountResponse.IsSucceeded = false;
				GeneralAccountResponse.Message = "User not found";
				return GeneralAccountResponse;
			}

			var otpCode  = await _otpManager.GenerateOtpAsync(dto.Email);


			var emailResponse = await _emailManager.SendEmailAsync(user.Email, "Your Password Reset OTP Code", $"Your OTP code for resetting your password is: {otpCode}");
			if (!emailResponse.IsSucceeded)
			{
				return emailResponse; // Return the email error if sending failed
			}
			
			GeneralAccountResponse.Message = "OTP sent successfully.";
			GeneralAccountResponse.IsSucceeded = emailResponse.IsSucceeded;
			return GeneralAccountResponse;
		}

		public async Task<GeneralAccountResponse> VerifyOtp(VerifyOtpRequestDto dto)
		{
			GeneralAccountResponse GeneralAccountResponse = new GeneralAccountResponse();

			// Retrieve email from cache using OTP
			if (!_cache.TryGetValue($"{dto.Email}_Verified", out string storedOtp) || storedOtp != dto.Otp)
			{
				GeneralAccountResponse.IsSucceeded = false;
				GeneralAccountResponse.Message = "Invalid or expired OTP";
				return GeneralAccountResponse;
			}

			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null)
			{
				GeneralAccountResponse.IsSucceeded = false;
				GeneralAccountResponse.Message = "User not found";
				return GeneralAccountResponse;
			}

			_cache.Set($"{dto.Email}_Verified", true, TimeSpan.FromMinutes(10));

			GeneralAccountResponse.IsSucceeded = true;
			GeneralAccountResponse.Message = "OTP verified successfully. You can now reset your password.";
			return GeneralAccountResponse;
		}

		public async Task<GeneralAccountResponse> ResetPasswordWithOtp(ResetPasswordRequestDto dto)
		{
			GeneralAccountResponse GeneralAccountResponse = new GeneralAccountResponse();

			if (!_cache.TryGetValue($"{dto.Email}_Verified", out bool otpVerified) || !otpVerified)
			{
				GeneralAccountResponse.IsSucceeded = false;
				GeneralAccountResponse.Message = "Invalid or expired OTP";
				return GeneralAccountResponse;
			}

			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null)
			{
				GeneralAccountResponse.IsSucceeded = false;
				GeneralAccountResponse.Message = "User not found";
				return GeneralAccountResponse;
			}

			var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, resetToken, dto.Password);
			if (!result.Succeeded)
			{
				GeneralAccountResponse.IsSucceeded = false;
				GeneralAccountResponse.Message = string.Join(", ", result.Errors.Select(e => e.Description));
				return GeneralAccountResponse;
			}

			await _otpManager.RemoveOtpAsync(dto.Email);

			var claims = await _userManager.GetClaimsAsync(user);
			GeneralAccountResponse = GeneralToken(claims);
			GeneralAccountResponse.IsSucceeded = true;
			GeneralAccountResponse.Message = "Password reset successfully";
			return GeneralAccountResponse;
		}

		private GeneralAccountResponse GeneralToken(IList<Claim> claims)
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

			return new GeneralAccountResponse
			{
				Token = token,
				ExpireDate = expireDate
			};
		}
	}
}

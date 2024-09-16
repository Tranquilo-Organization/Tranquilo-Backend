﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.BLL.Dtos.AccountDto;
using TranquiloSystem.BLL.Dtos.OtpDto;
using TranquiloSystem.DAL.Data.Models;

namespace TranquiloSystem.BLL.Manager.OtpManager
{
	public class OtpManager : IOtpManager
	{

		private readonly IMemoryCache _cache;

		public OtpManager(IMemoryCache cache)
		{
			_cache = cache;
		}
		public async Task<string> GenerateOtpAsync(string email)
		{
			var otp = new Random().Next(10000, 99999).ToString();

			_cache.Set($"{email}_Verified", otp, TimeSpan.FromMinutes(10)); 
			return otp;
		}
		public async Task RemoveOtpAsync(string email)
		{
			_cache.Remove($"{email}_Verified");
		}
	}
}
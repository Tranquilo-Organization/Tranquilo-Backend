﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranquiloSystem.BLL.Dtos.OtpDto.OtpDto
{
    public class SendOtpRequestDto
	{
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
	}
}

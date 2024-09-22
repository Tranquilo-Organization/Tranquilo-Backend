﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.BLL.Dtos.AccountDto;

namespace TranquiloSystem.BLL.Manager.EmailManager
{
    public interface IEmailManager
    {
		Task<GeneralResponse> SendEmailAsync(string recipientEmail, string subject, string body);

	}
}
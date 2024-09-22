using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.DAL.Data.Models;
using Org.BouncyCastle.Crypto.Macs;
using TranquiloSystem.BLL.Dtos.GeneralDto;

namespace TranquiloSystem.BLL.Manager.EmailManager
{
    public class EmailManager : IEmailManager
	{		
		private readonly SmtpSettings _smtpSettings;

		public EmailManager(IOptions<SmtpSettings> smtpSettings)
		{
			_smtpSettings = smtpSettings.Value;
			Console.WriteLine($"Host: {_smtpSettings.Host}");
			Console.WriteLine($"Port: {_smtpSettings.Port}");
			Console.WriteLine($"Username: {_smtpSettings.UserName}");
			Console.WriteLine($"Password: {_smtpSettings.Password}");
		}

		public async Task<GeneralAccountResponse> SendEmailAsync(string recipientEmail, string subject, string body)
		{
			GeneralAccountResponse response = new GeneralAccountResponse();
			
		
			try
			{
				var message = new MailMessage
				{
					From = new MailAddress(_smtpSettings.UserName, "Tranquilo Application"),
					To = { new MailAddress(recipientEmail) },
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				};

				using (var smtp = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
				{
					smtp.Credentials = new NetworkCredential(_smtpSettings.UserName,_smtpSettings.Password);
					smtp.EnableSsl = true;
					await smtp.SendMailAsync(message);
				}

				response.IsSucceeded = true;
				response.Message = "Email sent successfully.";
			}
			catch (Exception ex)
			{
				response.IsSucceeded = false;
				response.Message = $"Failed to send email: {ex.Message}";
			}

			return response;
		}
	}
}



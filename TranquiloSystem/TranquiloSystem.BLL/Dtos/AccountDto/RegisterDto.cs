using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranquiloSystem.BLL.Dtos.AccountDto
{
	public class RegisterDto
	{
		[MinLength(3)]
		public string UserName { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[MinLength(4)]
		public string Password { get; set; }

		[Compare("Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranquiloSystem.BLL.Dtos.GeneralDto
{
    public class GeneralAccountResponse
    {
        public bool IsSucceeded { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }

    }
}

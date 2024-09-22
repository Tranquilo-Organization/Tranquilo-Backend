using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TranquiloSystem.BLL.Dtos.GeneralDto
{
    public class GeneralResponseDto
    {
        public string? Message { get; set; }
        public bool IsSucceeded { get; set; }
        public object? Model { get; set; }
        [JsonIgnore]
        public ICollection<object>? Models { get; set; }
		public int? StatusCode { get; set; }
        public string? Notifcation { get; set; }
        public string? Object {  get; set; }
	}
}

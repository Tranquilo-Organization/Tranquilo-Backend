using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquilo.DAL.Data.Models
{
		public class SurveyAnswer
		{
			public int Id { get; set; } 
			public int QuestionId { get; set; } 
			public SurveyQuestion Question { get; set; } 
			public string UserId { get; set; } 
			public ApplicationUser User { get; set; } 
			public string Answer { get; set; }
		}
	
}

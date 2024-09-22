using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.DAL.Data.Enums;

namespace TranquiloSystem.BLL.Dtos.RoutineDto
{
	public class RoutineReadDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> Steps { get; set; }
		public string Type { get; set; } //morning,...
		public string LevelName { get; set; }
	}
}

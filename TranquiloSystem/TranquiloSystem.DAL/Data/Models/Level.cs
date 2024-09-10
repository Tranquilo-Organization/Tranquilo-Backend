using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquilo.DAL.Data.Models
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
		public int MinScore { get; set; }
		public int MaxScore { get; set; }
	}
}

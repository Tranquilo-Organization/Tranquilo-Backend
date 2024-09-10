using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquilo.DAL.Data.Models
{
    public class Routine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }
        public string Type { get; set; } //morning,...
       // public List<ApplicationUser> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.DAL.Data.Enums;

namespace Tranquilo.DAL.Data.Models
{
    public class UserRoutine
    { 
        public int Id { get; set; }
        public int RoutineId { get; set; }
        public Routine Routine { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public UserRoutineStatus Status { get; set; } = 0;
        public DateTime DateAssign { get; set; } = DateTime.Now;
        public DateTime? Date_Completed {  get; set; } 
       
    }
}

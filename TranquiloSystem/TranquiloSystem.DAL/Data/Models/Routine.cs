﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.DAL.Data.Enums;

namespace Tranquilo.DAL.Data.Models
{
    public class Routine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }
        public string Type { get; set; } //morning,...
        public List<ApplicationUser> Users { get; set; }

        [ForeignKey(nameof(Level))]
        public int? LevelId { get; set; }
        public Level Level { get; set; }
    }
}

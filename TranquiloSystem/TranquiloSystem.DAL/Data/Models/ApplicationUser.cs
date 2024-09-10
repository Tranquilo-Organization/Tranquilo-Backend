﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquilo.DAL.Data.Models
{
    public class ApplicationUser:IdentityUser
    {
        public  string NickName { get; set; }
        public List<Routine> Routines { get; set; } = new List<Routine>();
    }
}

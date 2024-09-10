using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.DAL.Data.DbHelper
{
    public class TranquiloContext : IdentityDbContext<ApplicationUser>
    {
        public TranquiloContext(DbContextOptions<TranquiloContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}

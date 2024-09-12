using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.DAL.Data.Models;

namespace TranquiloSystem.DAL.Data.DbHelper
{
    public class TranquiloContext : IdentityDbContext<ApplicationUser>
    {
        public TranquiloContext(DbContextOptions<TranquiloContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<UserRoutine> UserRoutines { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<UserScore> UserScores { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<BotConversation> BotConversations { get; set; }
        //public DbSet<Conversation> ChatBotInteractions { get; set; }


    }
}

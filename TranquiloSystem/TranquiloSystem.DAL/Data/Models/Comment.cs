using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquilo.DAL.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int PostID { get; set; }
        public Post Post { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.Entities
{
    public class Comment
    {
        public long CommentId { get; set; }

        public long PostId { get; set; }
        public Post Post { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}

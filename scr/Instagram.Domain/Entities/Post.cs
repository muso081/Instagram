using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.Entities
{
    public class Post
    {
        public long PostId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Media> Media { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public ICollection<Post> Posts { get; set; }
        public ICollection<UserFollower> Followers { get; set; } 
        public ICollection<UserFollower> Following { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }

        public enum UserRole
        {
            User,
            Admin,
            SuperAdmin
        }

    }
}

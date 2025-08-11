using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.Entities
{
    public class Media
    {
        public long MediaId { get; set; }

        public Post Post { get; set; }
        public long PostId { get; set; }

        public string Url { get; set; }

        public MediaTypeEnum MediaType { get; set; }
        
        public enum MediaTypeEnum
        {
            Image,
            Video
        }
    }
}

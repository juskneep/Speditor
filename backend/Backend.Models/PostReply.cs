using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Models
{
    public class PostReply
    {
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual Post Post { get; set; }
        public string PostId { get; set; }
    }
}

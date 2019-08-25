using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.DTOs
{
    public class PostReplyViewModel
    {
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
    }
}

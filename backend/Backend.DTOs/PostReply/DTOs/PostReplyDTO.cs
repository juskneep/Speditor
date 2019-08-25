using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.DTOs.PostReply.DTOs
{
    public class PostReplyDTO
    {
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
    }
}

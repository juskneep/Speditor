using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.DTOs
{
    public class PostDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public string ForumId { get; set; }
    }
}

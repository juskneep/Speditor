using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.DTOs
{
    public class ForumViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string ImageUrl { get; set; }
        public virtual IEnumerable<PostViewModel> Posts { get; set; }
    }
}

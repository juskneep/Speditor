using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Models
{
    public class Post
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        [MinLength(20)]
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual Forum Forum { get; set; }
        public string ForumId { get; set; }

        public virtual IEnumerable<PostReply> Replies { get; set; } 

    }
}

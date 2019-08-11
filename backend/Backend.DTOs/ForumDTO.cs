using Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.DTOs
{
    public class ForumDTO
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        public string ImageUrl { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }
    }
}

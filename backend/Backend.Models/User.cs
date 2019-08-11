using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Models
{
    public class User : IdentityUser
    {
        [Required]
        public override string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Company { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }

        public virtual IEnumerable<PostReply> PostReplies { get; set; }
    }
}

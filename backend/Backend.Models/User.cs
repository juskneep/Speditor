using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Username must be between 2 and 20 characters", MinimumLength = 2)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Company { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string Password { get; set; }    

        public virtual IEnumerable<Post> Posts { get; set; }

        public virtual IEnumerable<PostReply> PostReplies { get; set; }
    }
}

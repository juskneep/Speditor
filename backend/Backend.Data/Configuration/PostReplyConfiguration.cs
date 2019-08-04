using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Data.Configuration
{
    public class PostReplyConfiguration : IEntityTypeConfiguration<PostReply>
    {
        public void Configure(EntityTypeBuilder<PostReply> builder)
        {
            builder.HasKey(x => new { x.UserId, x.PostId});
        }
    }
}

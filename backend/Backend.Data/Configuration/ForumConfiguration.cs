using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Data.Configuration
{
    public class ForumConfiguration: IEntityTypeConfiguration<Forum>
    {
        public void Configure(EntityTypeBuilder<Forum> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.Forum)
                .HasForeignKey(x => x.ForumId);

        }   
    }
}

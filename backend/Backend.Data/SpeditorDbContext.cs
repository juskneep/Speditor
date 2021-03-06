﻿using Backend.Data.Configuration;
using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Data
{
    public class SpeditorDbContext : IdentityDbContext<User>
    {
        public SpeditorDbContext(DbContextOptions<SpeditorDbContext> context)
            :base(context)
        {

        }       

        public DbSet<Forum> Forums { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReply> PostReplies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           base.OnModelCreating(builder);
           builder.ApplyConfiguration(new ForumConfiguration());
           builder.ApplyConfiguration(new PostConfiguration());
           builder.ApplyConfiguration(new PostReplyConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}

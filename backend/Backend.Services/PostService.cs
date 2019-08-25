using AutoMapper;
using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class PostService
    {
        private readonly IMapper _mapper;
        private readonly SpeditorDbContext _context;

        public PostService(IMapper mapper, SpeditorDbContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<IEnumerable<PostViewModel>> GetAllPostsForUser(string userId)
        {
            return await this._context.Posts
                            .Where(x => x.UserId == userId)
                            .Include(x => x.Replies)
                            .Select(x => this._mapper.Map<PostViewModel>(x))
                            .ToListAsync();
        }

        public async Task<IEnumerable<PostViewModel>> GetAllPostsForForum(string forumId)
        {
            return await this._context.Posts
                            .Where(x => x.ForumId == forumId)
                            .Select(x => this._mapper.Map<PostViewModel>(x))
                            .ToListAsync();
        }

        public async Task<Post> CreatePost(PostDTO postModel)
        {
            var post = this._mapper.Map<Post>(postModel);

            var result = await this._context.AddAsync(post);
            await this._context.SaveChangesAsync();


            return result.Entity;
        }
    }
}

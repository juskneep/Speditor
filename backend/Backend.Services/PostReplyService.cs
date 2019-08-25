using AutoMapper;
using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.PostReply.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class PostReplyService
    {
        private readonly IMapper _mapper;
        private readonly SpeditorDbContext _context;

        public PostReplyService(IMapper mapper, SpeditorDbContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<IEnumerable<PostReplyViewModel>> GetPostReplyForGivenUserId(string userId)
        {
            return await this._context.PostReplies
                .Where(x => x.UserId == userId)
                .Select(x => this._mapper.Map<PostReplyViewModel>(x))
                .ToListAsync();
        }

        public async Task<IEnumerable<PostReplyViewModel>> GetPostReplyForGivenPostId(string postId)
        {
            return await this._context.PostReplies
                .Where(x => x.PostId == postId)
                .Select(x => this._mapper.Map<PostReplyViewModel>(x))
                .ToListAsync();
        }

        public async Task<PostReply> CreatePostReply(PostReplyDTO postReplyModel)
        {
            var postReply = this._mapper.Map<PostReply>(postReplyModel);
            var result = await this._context.AddAsync(postReply);
            await this._context.SaveChangesAsync();

            return result.Entity;
        }
    }
}

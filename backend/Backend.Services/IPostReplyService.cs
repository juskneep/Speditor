using Backend.DTOs;
using Backend.DTOs.PostReply.DTOs;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IPostReplyService
    {
        Task<IEnumerable<PostReplyViewModel>> GetPostReplyForGivenUserId(string userId);
        Task<IEnumerable<PostReplyViewModel>> GetPostReplyForGivenPostId(string postId);
        Task<PostReply> CreatePostReply(PostReplyDTO postReplyModel);
    }
}

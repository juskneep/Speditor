using Backend.DTOs;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> GetAllPostsForUser(string userId);
        Task<IEnumerable<PostViewModel>> GetAllPostsForForum(string forumId);
        Task<Post> CreatePost(PostDTO postModel);
    }
}

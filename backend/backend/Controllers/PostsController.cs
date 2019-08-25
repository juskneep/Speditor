using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            this._postService = postService;
        }


        [HttpGet("/forum/{forumId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<PostViewModel>> GetAllPostsByForum(string forumId)
        {
            return await this._postService.GetAllPostsForForum(forumId);
        }

        [HttpGet("users/{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<PostViewModel>> GetAllPostsByUser(string userId)
        {
            return await this._postService.GetAllPostsForUser(userId);
        }


        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreatePost(PostDTO model)
        {
            var result = await this._postService.CreatePost(model);

            if (result == null)
            {
                return BadRequest("Something went wrong :(");
            }
            else
            {
                return Ok("The forum theme was created!");
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.DTOs.PostReply.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsRepliesController : ControllerBase
    {
        private readonly IPostReplyService _postReplyService;

        public PostsRepliesController(IPostReplyService postReplyService)
        {
            this._postReplyService = postReplyService;
        }

        [HttpGet("{postId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<PostReplyViewModel>> GetPostRepliesByPostId(string postId)
        {
            return await this._postReplyService.GetPostReplyForGivenPostId(postId);
        }

        [HttpGet("/user/{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<PostReplyViewModel>> GetPostRepliesByUserId(string userId)
        {
            return await this._postReplyService.GetPostReplyForGivenUserId(userId);
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CreatePostReply(PostReplyDTO model)
        {
            var result = this._postReplyService.CreatePostReply(model);

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
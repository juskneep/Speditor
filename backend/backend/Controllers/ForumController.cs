using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumService _forumService;
        private readonly IMapper _mapper;
        public ForumController(IForumService forumService, IMapper mapper)
        {
            this._forumService = forumService;
            this._mapper = mapper;
        }

        [HttpGet("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<ForumViewModel>> GetAllPosts()
        {
            return this._forumService
                .GetAllForumThemes()
                .Select(x => _mapper.Map<ForumViewModel>(x))
                .ToList();

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ForumViewModel GetForum(string id)
        {
            var forum = this._forumService.GetForumThemeById(id);
            return forum;
        } 

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CreateForumTheme(ForumDTO forumDTO)
        {
            var forum = _forumService.CreateForumTheme(forumDTO);

            if (forum == null)
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
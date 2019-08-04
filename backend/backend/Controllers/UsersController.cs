using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService service)
        {
            this._userService = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterDTO user)
        {
            var result = await this._userService.RegisterUser(user);
            if (result != null)
            {
                return Ok("Okay man, you got it!");
            }
            else
            {
                return BadRequest("Wrong validation");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO user)
        {
            var result = await this._userService.LoginUser(user);
            if (result != null)
            {
                return Ok(new { Token = result });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
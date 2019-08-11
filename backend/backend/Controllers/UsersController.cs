using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Backend.WebApi.Models.ResponseModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UsersController(
            IMapper mapper,
            IConfiguration configuration,
            IAuthService authService,
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            this._mapper = mapper;
            this._configuration = configuration;
            this._authService = authService;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<object> RegisterUser(UserRegisterDTO model)
        {
            var user = this._mapper.Map<User>(model);
            user.PasswordHash = this._authService.GenerateHash(model.Password, "123");
            if (_userManager.Users.Any(u => u.UserName == user.UserName))
            {
                return new ErrorViewModel
                {
                    ErrorMessage = "The Username already exists. Please choose another one."
                };
            }
            if (await EmailExists(user.Email))
            {
                return new ErrorViewModel
                {
                    ErrorMessage = "Account with this email already exists!"
                };
            }
            var result = await this._userManager.CreateAsync(user);
            if (result.Succeeded)
            {

                var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                if (addToRoleResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);

                    return new
                    {
                        Message = "You have successfully registered.",
                        Token = this._authService.GenerateJwtToken(user)
                    };
                }
            }

            throw new Exception("Registartion isn't done!");
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) == null ? false : true;
        }

        [HttpPost("login")]
        public async Task<object> LoginUser(UserLoginDTO model)
        {
            var user =  this._userManager.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                return new ErrorViewModel
                {
                    ErrorMessage = "Invalid Password or Username!1"
                };
            }
            /*
            var password = this._authService.GenerateHash(model.Password, user.Salt);
            var result = await this._signInManager.PasswordSignInAsync(user, model.Password, false, false);
            */
            var password = this._authService.GenerateHash(model.Password, "123");


            if (user.PasswordHash == password)
            {
                return new AuthenticationViewModel
                {
                    Message = "You have successfully registered.",
                    Token = this._authService.GenerateJwtToken(user)
                };
            }

            return new ErrorViewModel
            {
                ErrorMessage = "Invalid Password or Username!2"
            };
        }
    }
}
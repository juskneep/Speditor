using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly SpeditorDbContext _context;
        private readonly UserManager<User> _userManager;

        public ValuesController(SpeditorDbContext context, UserManager<User> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        // GET: api/Values
        [HttpGet("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Values/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("role")]
        public async Task<object> GetRole(string userId)
        {
            var userFromDb = this._context.Users.FirstOrDefault(x => x.Id == userId);
            var userFromUserManager = await this._userManager.FindByIdAsync(userId);

            return Content($"UserFromDbRole: {string.Join(',', userFromDb.UserName)}. UserFromUserManagerRole: {string.Join(',', userFromUserManager.UserName)}");
        }

        // POST: api/Values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet("auth")]
        public object IsAuthenticated()
        {
            return Content("You are Authenticated! ");
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using AutoMapper;
using AutoMapper.Configuration;
using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly SpeditorDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(SpeditorDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this._context = context;
            this._mapper = mapper;
            this._configuration = configuration;
        }
        
        public async Task<string> RegisterUser(UserRegisterDTO model)
        {
            var user = this._mapper.Map<User>(model);
            user.Salt = CreateSalt(32);
            user.Password = ComputeSha256Hash(model.Password + user.Salt);
            await this._context.AddAsync<User>(user);
            await this._context.SaveChangesAsync();

            return "Success";
        }

        public async Task<string> LoginUser(UserLoginDTO model)
        {
            var user = this._context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                return "Invalid password or Username!";
            }
            var passwordHash = ComputeSha256Hash(model.Password + user.Salt);
            if (passwordHash != user.Password)
            {
                return "Invalid Password or Username!";
            }
            var token = this.GenerateJwt(user.Id);
            return token;
        }

        private string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        private string ComputeSha256Hash(string rawData)
        { 
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private string GenerateJwt(string userId)
        { 
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningSecret"]));
            var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new List<Claim> {
                    new Claim("userid", userId)
                }),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;            
        }
    }
}

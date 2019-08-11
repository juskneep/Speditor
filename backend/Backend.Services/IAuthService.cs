using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services
{
    public interface IAuthService
    {
        string CreateSalt(int size);
        string GenerateHash(string input, string salt);
        string GenerateJwtToken(User user);
    }
}

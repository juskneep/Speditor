using Backend.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IUserService
    {
        Task<string> LoginUser(UserLoginDTO model);

        Task<string> RegisterUser(UserRegisterDTO model);
    }
}

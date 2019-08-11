using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.WebApi.Models.ResponseModels
{
    public class AuthenticationViewModel
    {
        public string Message { get; set; }
        public string Token { get; set; }
    }
}

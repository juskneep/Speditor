using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services.Settings
{
    public class JwtSettings
    {
        public string SigningSecret { get; set; }
        public string ExpireDays { get; set; }
    }
}

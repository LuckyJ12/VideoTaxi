using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class LoginResult
    {
        public string LoginSuccess { get; set; }

        public string LoginFailure { get; set; }

        public string? Token { get; set; }
    }
}

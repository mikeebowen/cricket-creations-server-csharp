using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Avatar { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class CheckRefreshTokenRequest
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
    }
}

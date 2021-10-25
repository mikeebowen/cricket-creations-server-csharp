using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class NewUser
    {

        //[Required]
        //[MaxLength(200)]
        public string Name { get; set; }
        public string Surname { get; set; }
        //[Required]
        //[MaxLength(200)]
        //[EmailAddress]
        public string Email { get; set; }
        //[Required]
        //[MaxLength(200)]
        public string UserName { get; set; }
        //[Required]
        //[MaxLength(200)]
        public string Password { get; set; }
    }
}

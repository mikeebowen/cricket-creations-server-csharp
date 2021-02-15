using CricketCreationsDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketCreationsRepository.Models
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Created { get; set; }
        public string LastUpdated { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }

        public AuthenticationResponse(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Created = user.Created.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            LastUpdated = user.LastUpdated.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            Email = user.Email;
            Avatar = user.Avatar;
            Token = token;
        }
    }
}

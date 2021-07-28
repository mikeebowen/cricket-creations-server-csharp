using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreationsDatabase.Models
{
    public enum Role
    {
        Administrator,
        User
    }
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public byte[] Salt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public bool Deleted { get; set; } = false;
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        public string Surname { get; set; }
        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(200)]
        public string UserName { get; set; }
        [DefaultValue(Role.User)]
        public Role Role { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Page> Pages { get; set; }
        public List<Image> Images { get; set; }
        public Image Avatar { get; set; }
    }
}

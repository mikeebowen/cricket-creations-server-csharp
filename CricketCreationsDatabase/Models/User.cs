using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreationsDatabase.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string SurName { get; set; }
        [Required]
        [MaxLength(200)] 
        [EmailAddress]
        public string Email { get; set; }
        public string Avatar { get; set; }
        public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}

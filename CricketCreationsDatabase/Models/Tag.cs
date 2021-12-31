using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CricketCreationsDatabase.Models
{
    public class Tag : BaseEntity
    {
        public bool Deleted { get; set; } = false;

        public List<User> Users { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public List<BlogPost> BlogPosts { get; set; }
    }
}

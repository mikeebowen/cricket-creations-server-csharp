using System.Collections.Generic;

namespace CricketCreationsDatabase.Models
{
    public class Tag : BaseEntity
    {
        public bool Deleted { get; set; } = false;

        public User User { get; set; }

        public string Name { get; set; }

        public List<BlogPost> BlogPosts { get; set; }
    }
}

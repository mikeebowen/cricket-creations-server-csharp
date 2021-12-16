using System.Collections.Generic;

namespace CricketCreationsDatabase.Models
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public User User { get; set; }

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public List<Tag> Tags { get; set; }
    }
}

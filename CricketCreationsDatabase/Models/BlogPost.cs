using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CricketCreationsDatabase.Models
{
    public class BlogPost : BaseEntity
    {
        [MaxLength(300)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string Subtitle { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public User User { get; set; }

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public List<Tag> Tags { get; set; }
    }
}

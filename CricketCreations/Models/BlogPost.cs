using CricketCreations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class BlogPost
    {
        public int? Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public UserService User { get; set; }
        public bool Published { get; set; }
        public List<TagService> Tags { get; set; }

        BlogPost()
        {
            Tags = new List<TagService>();
        }
    }
}

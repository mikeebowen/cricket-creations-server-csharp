using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CricketCreations.Models
{
    public class BlogPost
    {
        public int? Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Author { get; set; }

        [Required]
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public bool Published { get; set; } = false;

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}

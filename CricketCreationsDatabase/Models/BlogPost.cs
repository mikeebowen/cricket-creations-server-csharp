using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

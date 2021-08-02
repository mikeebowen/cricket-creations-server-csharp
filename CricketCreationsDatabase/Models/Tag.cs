using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CricketCreationsDatabase.Models
{
    public class Tag : BaseEntity
    {
        public bool Deleted { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public List<BlogPost> BlogPosts { get; set; }

        public Tag()
        {
            this.Deleted = false;
            this.BlogPosts = new List<BlogPost>();
        }
    }
}

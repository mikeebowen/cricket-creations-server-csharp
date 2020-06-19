using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class BlogPost
    {
        //  id: this.attr(null),
        //author_id: this.attr(null),
        //created: this.attr(null),
        //lastUpdated: this.attr(null),
        //title: this.string (''),
        //article: this.string (''),
        //image: this.attr(null),
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string Image { get; set; }
        // Foreign Key
        public int UserId { get; set; }
        // Navigation Property
        public User User { get; set; }
    }
}

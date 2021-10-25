using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CricketCreations.Models
{
    public class Tag
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

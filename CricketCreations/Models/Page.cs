using System;
using System.ComponentModel.DataAnnotations;

namespace CricketCreations.Models
{
    public class Page
    {
        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; } = false;
        public bool Published { get; set; } = false;
        [Required, IsUniquePageHeading]
        public string Heading { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

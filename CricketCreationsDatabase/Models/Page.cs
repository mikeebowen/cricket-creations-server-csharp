using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CricketCreationsDatabase.Models
{
    public class Page
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Deleted { get; set; } = false;
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
    }
}

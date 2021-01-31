using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CricketCreationsDatabase.Models
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Deleted { get; set; } = false;
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public ICollection<BlogPostTag> BlogPostTags { get; set; }
    }
}

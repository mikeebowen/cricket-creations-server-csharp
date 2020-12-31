using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CricketCreationsDatabase.Models
{
    public class BlogPostTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Models
{
    public class TagDTO
    {
        [Key]
        public int? Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Deleted { get; set; } = false;
        public string Name { get; set; }
        public ICollection<BlogPostDTO> BlogPosts { get; set; }
    }
}

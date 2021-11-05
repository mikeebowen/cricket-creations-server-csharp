using CricketCreationsRepository.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketCreationsRepository.Models
{
    public class BlogPostDTO
    {
        [Key]
        public int? Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public bool Published { get; set; } = false;
        public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
    }
}

using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CricketCreationsRepository.Models
{
    public class TagDTO
    {
        private HtmlSanitizer _htmlSanitizer = new HtmlSanitizer();
        private string _name;

        [Key]
        public int? Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool Deleted { get; set; } = false;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = _htmlSanitizer.Sanitize(value);
            }
        }

        public ICollection<BlogPostDTO> BlogPosts { get; set; }
    }
}

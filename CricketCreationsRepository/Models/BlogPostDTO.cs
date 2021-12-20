using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CricketCreationsRepository.Models
{
    public class BlogPostDTO
    {
        private HtmlSanitizer _htmlSanitizer = new HtmlSanitizer();
        private string _content;
        private string _title;

        [Key]
        public int? Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = _htmlSanitizer.Sanitize(value);
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = _htmlSanitizer.Sanitize(value);
            }
        }

        public string Image { get; set; }

        public bool Published { get; set; } = false;

        public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
    }
}

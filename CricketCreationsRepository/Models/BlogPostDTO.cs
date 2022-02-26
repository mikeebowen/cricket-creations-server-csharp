using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ganss.XSS;

namespace CricketCreationsRepository.Models
{
    public class BlogPostDTO
    {
        private HtmlSanitizer _htmlSanitizer = new HtmlSanitizer();
        private string _content;
        private string _title;
        private string _subtitle;

        [Key]
        public int? Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Author { get; set; }

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

        public string Subtitle
        {
            get
            {
                return _subtitle;
            }

            set
            {
                _subtitle = _htmlSanitizer.Sanitize(value);
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
                _htmlSanitizer.AllowedSchemes.Add("data");
                _content = _htmlSanitizer.Sanitize(value);
            }
        }

        public string Image { get; set; }

        public bool Published { get; set; } = false;

        public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
    }
}

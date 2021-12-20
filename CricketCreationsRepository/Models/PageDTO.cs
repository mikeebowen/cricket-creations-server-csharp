using Ganss.XSS;
using System;
using System.ComponentModel.DataAnnotations;

namespace CricketCreationsRepository.Models
{
    public class PageDTO
    {
        private HtmlSanitizer _htmlSanitizer = new HtmlSanitizer();
        private string _content;
        private string _heading;
        private string _title;

        [Key]
        public int Id { get; set; }

        public bool Deleted { get; set; }

        public bool Published { get; set; }

        public string Heading
        {
            get
            {
                return _heading;
            }

            set
            {
                _heading = _htmlSanitizer.Sanitize(value);
            }
        }

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

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

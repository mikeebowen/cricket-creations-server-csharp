using System.ComponentModel.DataAnnotations;

namespace CricketCreationsDatabase.Models
{
    public class Page : BaseEntity
    {
        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public User User { get; set; }

        [MaxLength(300)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string Heading { get; set; }

        public string Content { get; set; }
    }
}

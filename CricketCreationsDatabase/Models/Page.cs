namespace CricketCreationsDatabase.Models
{
    public class Page : BaseEntity
    {
        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public User User { get; set; }

        public string Title { get; set; }

        public string Heading { get; set; }

        public string Content { get; set; }
    }
}

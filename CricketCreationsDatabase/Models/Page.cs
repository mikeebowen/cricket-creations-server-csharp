using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CricketCreationsDatabase.Models
{
    public class Page : BaseEntity
    {
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }

        public Page()
        {
            this.Deleted = false;
            this.Published = false;
        }
    }
}

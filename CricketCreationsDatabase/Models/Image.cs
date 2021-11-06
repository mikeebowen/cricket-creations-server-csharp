using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CricketCreationsDatabase.Models
{
    public class Image : BaseEntity
    {
        public bool Deleted { get; set; } = false;
        public User User { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
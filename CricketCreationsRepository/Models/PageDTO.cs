﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CricketCreationsRepository.Models
{
    public class PageDTO
    {
        [Key]
        public int Id { get; set; }

        public bool Deleted { get; set; }

        public bool Published { get; set; }

        public string Heading { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

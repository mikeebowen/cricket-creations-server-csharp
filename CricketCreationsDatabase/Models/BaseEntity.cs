using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CricketCreationsDatabase.Models
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}

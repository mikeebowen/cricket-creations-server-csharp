namespace CricketCreations.Models
{
    public class NewUser
    {
        // [Required]
        // [MaxLength(200)]
        public string Name { get; set; }

        public string Surname { get; set; }

        // [Required]
        // [MaxLength(200)]
        // [EmailAddress]
        public string Email { get; set; }

        // [Required]
        // [MaxLength(200)]
        public string UserName { get; set; }

        // [Required]
        // [MaxLength(200)]
        public string Password { get; set; }

        [IsValidBase64]
        public string Avatar { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBasket.Models
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }  // Required for unique identification
        
        [Required]
        [EmailAddress] // Ensures valid email format
        public string Email { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}

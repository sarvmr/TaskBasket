using System.ComponentModel.DataAnnotations;

namespace TaskBasket.Models
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

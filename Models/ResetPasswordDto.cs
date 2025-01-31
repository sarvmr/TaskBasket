using System.ComponentModel.DataAnnotations;

namespace TaskBasket.Models
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }  // Reset token

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}

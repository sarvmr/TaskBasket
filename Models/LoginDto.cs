using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBasket.Models
{
  public class LoginDto
  {
    [Required]
    public string UsernameOrEmail { get; set; }  // Accepts either username OR email

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
  }
}


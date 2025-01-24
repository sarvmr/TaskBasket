using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBasket.Models{
  public class LoginDto{
    [Required]    
    public string Username { get; set; }
    [Required]
    [MinLength(6)]   
    public string Password { get; set; }
  }
}
 

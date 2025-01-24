using System;

namespace TaskBasket.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

         // Navigation property for related tasks
         public ICollection<Task> Tasks { get; set; }
    }
}
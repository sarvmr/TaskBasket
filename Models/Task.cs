using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBasket.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment for int primary keys
        public int Id { get; set; } // Ensure this property has both getter and setter

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public string Priority { get; set; } = "Medium";

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key for related user
        public int? UserId { get; set; }

        // Navigation property for related user
        public User? User { get; set; }
    }
}

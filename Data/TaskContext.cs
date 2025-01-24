using Microsoft.EntityFrameworkCore;
using TaskBasket.Models;

namespace TaskBasket.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        // DbSet for Task entity
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User entity
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Task entity
            modelBuilder.Entity<TaskBasket.Models.Task>(entity =>
            {
                // Primary key configuration
                entity.HasKey(t => t.Id);

                // Auto-increment configuration for Id
                entity.Property(t => t.Id)
                      .ValueGeneratedOnAdd();

                // Required fields
                entity.Property(t => t.Title)
                      .IsRequired()
                      .HasMaxLength(255); // Optional: Limit title length

                entity.Property(t => t.Status)
                      .IsRequired()
                      .HasDefaultValue("Pending");

                entity.Property(t => t.Priority)
                      .IsRequired()
                      .HasDefaultValue("Medium");

                // Optional fields
                entity.Property(t => t.Description)
                      .HasMaxLength(1000); // Optional: Limit description length

                // Timestamps
                entity.Property(t => t.CreatedAt)
                        .ValueGeneratedOnAdd();
                      

                entity.Property(t => t.UpdatedAt)
                        .ValueGeneratedOnAddOrUpdate();
            });
        }
    }
}

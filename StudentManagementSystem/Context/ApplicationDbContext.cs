using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Branch> Branches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(s => s.Id).ValueGeneratedOnAdd();
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(50);
                entity.Property(c => c.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(b => b.Id).ValueGeneratedOnAdd();
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(50);
                entity.Property(b => b.Location).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Students)
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Branch)
                .WithMany(b => b.Courses)
                .HasForeignKey(c => c.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        [Key]
        //auto increment
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int CourseId { get; set; }
        public int BranchId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Branch? Branch { get; set; }
    }
}
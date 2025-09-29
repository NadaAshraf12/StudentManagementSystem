using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int BranchId { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
    }
}
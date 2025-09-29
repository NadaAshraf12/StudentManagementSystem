using StudentManagementSystem.Models;

namespace StudentManagementSystem.Context
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Branches.Any())
            {
                return; 
            }

            var branches = new Branch[]
            {
                new Branch { Title = "Main Branch", Location = "Cairo" },
                new Branch { Title = "Secondary Branch", Location = "Alexandria" }
            };

            context.Branches.AddRange(branches);
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course { Title = "Computer Science", BranchId = branches[0].Id },
                new Course { Title = "Engineering", BranchId = branches[1].Id }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            var students = new Student[]
            {
                new Student { Name = "Ahmed Mohamed", Address = "123 Main St", CourseId = courses[0].Id, BranchId = branches[0].Id },
                new Student { Name = "Sarah Ali", Address = "456 Park Ave", CourseId = courses[1].Id, BranchId = branches[1].Id }
            };

            context.Students.AddRange(students);
            context.SaveChanges();
        }
    }
}

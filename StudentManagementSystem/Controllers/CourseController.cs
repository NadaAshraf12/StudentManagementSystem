using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Context;

namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Students)
                .Select(c => new CourseViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    StudentCount = c.Students != null ? c.Students.Count : 0
                })
                .ToListAsync();

            return View(courses);
        }
    }

    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int StudentCount { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Context;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Include(s => s.Branch)
                .Include(s => s.Course)
                .Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    CreatedAt = s.CreatedAt,
                    BranchName = s.Branch != null ? s.Branch.Title : "N/A",
                    CourseName = s.Course != null ? s.Course.Title : "N/A"
                })
                .ToListAsync();

            return View(students);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Branch)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                CreatedAt = student.CreatedAt,
                BranchName = student.Branch != null ? student.Branch.Title : "N/A",
                CourseName = student.Course != null ? student.Course.Title : "N/A",
                BranchId = student.BranchId,
                CourseId = student.CourseId
            };

            await PopulateViewBags();
            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    CreatedAt = DateTime.Now,
                    BranchId = viewModel.BranchId,
                    CourseId = viewModel.CourseId
                };

                _context.Add(student);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Student created successfully!";
                return RedirectToAction(nameof(Index));
            }

            await PopulateViewBags();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                BranchId = student.BranchId,
                CourseId = student.CourseId
            };

            await PopulateViewBags();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var student = await _context.Students.FindAsync(id);
                    if (student == null)
                    {
                        return NotFound();
                    }

                    student.Name = viewModel.Name;
                    student.Address = viewModel.Address;
                    student.BranchId = viewModel.BranchId;
                    student.CourseId = viewModel.CourseId;

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Student updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            await PopulateViewBags();
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Branch)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                CreatedAt = student.CreatedAt,
                BranchName = student.Branch != null ? student.Branch.Title : "N/A",
                CourseName = student.Course != null ? student.Course.Title : "N/A"
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Student deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        private async Task PopulateViewBags()
        {
            var branches = await _context.Branches.ToListAsync();
            ViewBag.Branches = new SelectList(branches, "Id", "Title");

            var courses = await _context.Courses.ToListAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Title");
        }
    }

    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;

        public int BranchId { get; set; }
        public int CourseId { get; set; }
    }
}
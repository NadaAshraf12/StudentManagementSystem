using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IBranchService _branchService;
        private readonly ICourseService _courseService;

        public StudentController(
            IStudentService studentService,
            IBranchService branchService,
            ICourseService courseService)
        {
            _studentService = studentService;
            _branchService = branchService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            var viewModel = students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                CreatedAt = s.CreatedAt,
                BranchName = s.Branch?.Title ?? "N/A",
                CourseName = s.Course?.Title ?? "N/A"
            });

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var student = await _studentService.GetStudentByIdAsync(id.Value);
            if (student == null) return NotFound();

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                CreatedAt = student.CreatedAt,
                BranchName = student.Branch?.Title ?? "N/A",
                CourseName = student.Course?.Title ?? "N/A",
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

                await _studentService.CreateStudentAsync(student);
                TempData["SuccessMessage"] = "Student created successfully!";
                return RedirectToAction(nameof(Index));
            }

            await PopulateViewBags();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var student = await _studentService.GetStudentByIdAsync(id.Value);
            if (student == null) return NotFound();

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
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var student = await _studentService.GetStudentByIdAsync(id);
                    if (student == null) return NotFound();

                    student.Name = viewModel.Name;
                    student.Address = viewModel.Address;
                    student.BranchId = viewModel.BranchId;
                    student.CourseId = viewModel.CourseId;

                    await _studentService.UpdateStudentAsync(student);
                    TempData["SuccessMessage"] = "Student updated successfully!";
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            await PopulateViewBags();
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var student = await _studentService.GetStudentByIdAsync(id.Value);
            if (student == null) return NotFound();

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                CreatedAt = student.CreatedAt,
                BranchName = student.Branch?.Title ?? "N/A",
                CourseName = student.Course?.Title ?? "N/A"
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            TempData["SuccessMessage"] = "Student deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateViewBags()
        {
            var branches = await _branchService.GetAllBranchesAsync();
            ViewBag.Branches = new SelectList(branches, "Id", "Title");

            var courses = await _courseService.GetAllCoursesAsync();
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

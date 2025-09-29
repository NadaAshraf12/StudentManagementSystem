using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Context;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var branches = await _context.Branches
                .Select(b => new BranchViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Location = b.Location
                })
                .ToListAsync();

            return View(branches);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .FirstOrDefaultAsync(m => m.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            var viewModel = new BranchViewModel
            {
                Id = branch.Id,
                Title = branch.Title,
                Location = branch.Location
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BranchViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var branch = new Branch
                {
                    Title = viewModel.Title,
                    Location = viewModel.Location
                };

                _context.Add(branch);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Branch created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            var viewModel = new BranchViewModel
            {
                Id = branch.Id,
                Title = branch.Title,
                Location = branch.Location
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BranchViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var branch = await _context.Branches.FindAsync(id);
                    if (branch == null)
                    {
                        return NotFound();
                    }

                    branch.Title = viewModel.Title;
                    branch.Location = viewModel.Location;

                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Branch updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(viewModel.Id))
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
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .Include(b => b.Students) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            if (branch.Students.Any())
            {
                ViewBag.ErrorMessage = $"Cannot delete branch '{branch.Title}' because it has {branch.Students.Count} student(s). Please delete or move the students first.";
            }

            var viewModel = new BranchViewModel
            {
                Id = branch.Id,
                Title = branch.Title,
                Location = branch.Location
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.Branches
                .Include(b => b.Students) 
                .FirstOrDefaultAsync(b => b.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            if (branch.Students.Any())
            {
                TempData["ErrorMessage"] = $"Cannot delete branch '{branch.Title}' because it has {branch.Students.Count} student(s). Please delete or move the students first.";
                return RedirectToAction(nameof(Index));
            }

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Branch deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(int id)
        {
            return _context.Branches.Any(e => e.Id == id);
        }
    }

    public class BranchViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchRepository _branchRepository;

        public BranchController(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<IActionResult> Index()
        {
            var branches = await _branchRepository.GetAllAsync();
            var viewModel = branches.Select(b => new BranchViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Location = b.Location
            });

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchRepository.GetByIdAsync(id.Value);
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

                await _branchRepository.AddAsync(branch);
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

            var branch = await _branchRepository.GetByIdAsync(id.Value);
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
                    var branch = await _branchRepository.GetByIdAsync(id);
                    if (branch == null)
                    {
                        return NotFound();
                    }

                    branch.Title = viewModel.Title;
                    branch.Location = viewModel.Location;

                    await _branchRepository.UpdateAsync(branch);
                    TempData["SuccessMessage"] = "Branch updated successfully!";
                }
                catch (Exception)
                {
                    if (!await _branchRepository.ExistsAsync(viewModel.Id))
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

            var branch = await _branchRepository.GetByIdAsync(id.Value);
            if (branch == null)
            {
                return NotFound();
            }

            if (await _branchRepository.HasStudentsAsync(id.Value))
            {
                ViewBag.ErrorMessage = $"Cannot delete branch '{branch.Title}' because it has students. Please delete or move the students first.";
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
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            if (await _branchRepository.HasStudentsAsync(id))
            {
                TempData["ErrorMessage"] = $"Cannot delete branch '{branch.Title}' because it has students. Please delete or move the students first.";
                return RedirectToAction(nameof(Index));
            }

            await _branchRepository.DeleteAsync(id);
            TempData["SuccessMessage"] = "Branch deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }

    public class BranchViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
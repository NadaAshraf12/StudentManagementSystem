using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Context;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _context.Branches
                .Include(b => b.Students)
                .Include(b => b.Courses)
                .ToListAsync();
        }

        public async Task<Branch> GetByIdAsync(int id)
        {
            return await _context.Branches
                .Include(b => b.Students)
                .Include(b => b.Courses)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Branch> AddAsync(Branch branch)
        {
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task<Branch> UpdateAsync(Branch branch)
        {
            _context.Branches.Update(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task DeleteAsync(int id)
        {
            var branch = await GetByIdAsync(id);
            if (branch != null)
            {
                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Branches.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> HasStudentsAsync(int branchId)
        {
            return await _context.Students.AnyAsync(s => s.BranchId == branchId);
        }
    }
}
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Interfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAllAsync();
        Task<Branch> GetByIdAsync(int id);
        Task<Branch> AddAsync(Branch branch);
        Task<Branch> UpdateAsync(Branch branch);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> HasStudentsAsync(int branchId);
    }
}
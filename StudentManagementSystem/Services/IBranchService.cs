using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public interface IBranchService
    {
        Task<IEnumerable<Branch>> GetAllBranchesAsync();
        Task<Branch> GetBranchByIdAsync(int id);
        Task<Branch> CreateBranchAsync(Branch branch);
        Task<Branch> UpdateBranchAsync(Branch branch);
        Task DeleteBranchAsync(int id);
        Task<bool> CanDeleteBranchAsync(int branchId);
    }
}
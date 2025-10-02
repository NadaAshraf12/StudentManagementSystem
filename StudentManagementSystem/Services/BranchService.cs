using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<IEnumerable<Branch>> GetAllBranchesAsync()
        {
            return await _branchRepository.GetAllAsync();
        }

        public async Task<Branch> GetBranchByIdAsync(int id)
        {
            return await _branchRepository.GetByIdAsync(id);
        }

        public async Task<Branch> CreateBranchAsync(Branch branch)
        {
            return await _branchRepository.AddAsync(branch);
        }

        public async Task<Branch> UpdateBranchAsync(Branch branch)
        {
            return await _branchRepository.UpdateAsync(branch);
        }

        public async Task DeleteBranchAsync(int id)
        {
            await _branchRepository.DeleteAsync(id);
        }

        public async Task<bool> CanDeleteBranchAsync(int branchId)
        {
            return !await _branchRepository.HasStudentsAsync(branchId);
        }
    }
}
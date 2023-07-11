
using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IBranchesRepository
    {
        public Task<IEnumerable<Branch>> GetAllBranches(IDbTransaction transaction);
        public Task<int> CreateBranch(Branch cinemaBranch, IDbTransaction transaction);
        Task<Branch> GetById(int id, IDbTransaction transaction);
        public Task Delete(int id, IDbTransaction transaction);
        Task Update(Branch branch, IDbTransaction transaction);
    }
}

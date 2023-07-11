using CineNet.Domain.Entities;
using System.Data;

namespace CineNet.Domain.Contracts
{
    public interface IEmailTasksRepository
    {
        Task Create(EmailTask task, IDbTransaction transaction);
        Task Delete(int id, IDbTransaction transaction);
        Task<IEnumerable<EmailTask>> Get(IDbTransaction transaction);
    }
}

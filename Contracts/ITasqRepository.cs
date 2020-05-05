using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITasqRepository
    {
        Task<IEnumerable<Tasq>> GetAllTasqsAsync(bool trackChanges);
        Task<Tasq> GetTasqAsync(Guid tasqId, bool trackChanges);
        Task<IEnumerable<Tasq>> GetChildrenAsync(Guid tasqId, bool trachChanges);
        Task<Tasq> GetChildAsync(Guid tasqId, Guid childId, bool trackChanges);
        void CreateTasq(Tasq tasq);
        void CreateChildTasq(Guid parentId, Tasq tasq);
        Task<IEnumerable<Tasq>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteTasq(Tasq tasq);
    }
}

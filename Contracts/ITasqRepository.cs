using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITasqRepository
    {
        Task<PagedList<Tasq>> GetAllTasqsAsync(TasqParameters tasqParameters, bool trackChanges);
        Task<Tasq> GetTasqAsync(Guid tasqId, bool trackChanges);
        Task<PagedList<Tasq>> GetChildrenAsync(Guid tasqId, TasqParameters tasqParameters, bool trackChanges);
        Task<Tasq> GetChildAsync(Guid tasqId, Guid childId, bool trackChanges);
        void CreateTasq(Tasq tasq);
        void CreateChildTasq(Guid parentId, Tasq tasq);
        Task<IEnumerable<Tasq>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteTasq(Tasq tasq);
    }
}

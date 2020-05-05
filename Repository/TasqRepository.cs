using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TasqRepository : RepositoryBase<Tasq>, ITasqRepository
    {
        public TasqRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Tasq>> GetAllTasqsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(t => t.Name)
            .ToListAsync();

        public async Task<Tasq> GetTasqAsync(Guid tasqId, bool trackChanges) =>
            await FindByCondition(t => t.Id.Equals(tasqId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Tasq>> GetChildrenAsync(Guid tasqId, bool trackChanges) =>
            await FindByCondition(t => t.ParentId.Equals(tasqId), trackChanges)
            .OrderBy(t => t.Name)
            .ToListAsync();

        public async Task<Tasq> GetChildAsync(Guid mainId, Guid childId, bool trackChanges) =>
            await FindByCondition(t => t.ParentId.Equals(mainId) && t.Id.Equals(childId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateTasq(Tasq tasq) => Create(tasq);

        public void CreateChildTasq(Guid parentId, Tasq tasq)
        {
            tasq.ParentId = parentId;
            Create(tasq);
        }

        public async Task<IEnumerable<Tasq>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

        public void DeleteTasq(Tasq tasq)
        {
            Delete(tasq);
        }
    }
}

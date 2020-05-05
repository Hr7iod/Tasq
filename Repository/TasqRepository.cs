using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class TasqRepository : RepositoryBase<Tasq>, ITasqRepository
    {
        public TasqRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public IEnumerable<Tasq> GetAllTasqs(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(t => t.Name)
            .ToList();

        public Tasq GetTasq(Guid tasqId, bool trackChanges) =>
            FindByCondition(t => t.Id.Equals(tasqId), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Tasq> GetChildren(Guid tasqId, bool trackChanges) =>
            FindByCondition(t => t.ParentId.Equals(tasqId), trackChanges)
            .OrderBy(t => t.Name);

        public Tasq GetChild(Guid mainId, Guid childId, bool trackChanges) =>
            FindByCondition(t => t.ParentId.Equals(mainId) && t.Id.Equals(childId), trackChanges)
            .SingleOrDefault();

        public void CreateTasq(Tasq tasq) => Create(tasq);

        public void CreateChildTasq(Guid parentId, Tasq tasq)
        {
            tasq.ParentId = parentId;
            Create(tasq);
        }

        public IEnumerable<Tasq> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();

        public void DeleteTasq(Tasq tasq)
        {
            Delete(tasq);
        }
    }
}

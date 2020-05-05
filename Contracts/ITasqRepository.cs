using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ITasqRepository
    {
        IEnumerable<Tasq> GetAllTasqs(bool trackChanges);
        Tasq GetTasq(Guid tasqId, bool trackChanges);
        IEnumerable<Tasq> GetChildren(Guid tasqId, bool trachChanges);
        Tasq GetChild(Guid tasqId, Guid childId, bool trackChanges);
        void CreateTasq(Tasq tasq);
        void CreateChildTasq(Guid parentId, Tasq tasq);
        IEnumerable<Tasq> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteTasq(Tasq tasq);
    }
}

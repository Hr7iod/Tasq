﻿using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Repository.Extensions;
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

        public async Task<PagedList<Tasq>> GetAllTasqsAsync(TasqParameters tasqParameters, bool trackChanges)
        {
            var tasqs = await FindAll(trackChanges)
            .FilterTasq(tasqParameters.MinProgress, tasqParameters.MaxProgress)
            .SearchName(tasqParameters.SearchName)
            .SearchParent(tasqParameters.SearchParent)
            .Sort(tasqParameters.OrderBy)
            .ToListAsync();


            return PagedList<Tasq>.ToPagedList(tasqs, tasqParameters.PageNumber, tasqParameters.PageSize);
        }

        public async Task<Tasq> GetTasqAsync(Guid tasqId, bool trackChanges) =>
            await FindByCondition(t => t.Id.Equals(tasqId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<PagedList<Tasq>> GetChildrenAsync(Guid tasqId, TasqParameters tasqParameters, bool trackChanges)
        {
            var children = await FindByCondition(t => t.ParentId.Equals(tasqId), trackChanges)
                .FilterTasq(tasqParameters.MinProgress, tasqParameters.MaxProgress)
                .SearchName(tasqParameters.SearchName)
                .SearchParent(tasqParameters.SearchParent)
                .Sort(tasqParameters.OrderBy)
                .ToListAsync();

            return PagedList<Tasq>
                .ToPagedList(children, tasqParameters.PageNumber, tasqParameters.PageSize);
        }

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

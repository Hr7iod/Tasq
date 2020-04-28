using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ITasqRepository _tasqRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ITasqRepository Tasq
        {
            get
            {
                if (_tasqRepository == null)
                    _tasqRepository = new TasqRepository(_repositoryContext);

                return _tasqRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}

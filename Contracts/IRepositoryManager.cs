using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ITasqRepository Tasq { get; }
        void Save();
    }
}

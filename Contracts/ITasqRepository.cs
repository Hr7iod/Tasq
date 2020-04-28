using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ITasqRepository
    {
        IEnumerable<Tasq> GetAllTasqs(bool trackChanges);
    }
}

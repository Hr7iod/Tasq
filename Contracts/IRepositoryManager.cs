﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ITasqRepository Tasq { get; }
        Task SaveAsync();
    }
}

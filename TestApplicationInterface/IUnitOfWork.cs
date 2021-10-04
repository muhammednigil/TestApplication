using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestApplicationInterface
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}

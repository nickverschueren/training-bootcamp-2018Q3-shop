using System;
using System.Threading.Tasks;

namespace Shop.Api.Data
{
    public interface ITransactionManager : IDisposable
    {
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
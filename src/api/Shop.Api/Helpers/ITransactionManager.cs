using System;
using System.Threading.Tasks;

namespace Shop.Api.Helpers
{
    public interface ITransactionManager : IDisposable
    {
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
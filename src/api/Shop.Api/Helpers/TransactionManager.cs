using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shop.Api.Helpers
{
    public class TransactionManager<T> : ITransactionManager where T : DbContext
    {
        private readonly T _dbContext;
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private IDbContextTransaction _dbTransaction;

        public TransactionManager(T dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransaction()
        {
            try
            {
                await _lock.WaitAsync();

                if (_dbTransaction != null)
                    throw new InvalidOperationException("A transaction has already been started");

                _dbTransaction = await _dbContext.Database.BeginTransactionAsync();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task CommitTransaction()
        {
            try
            {
                await _lock.WaitAsync();

                if (_dbTransaction == null)
                    throw new InvalidOperationException("No transaction is running");

                _dbTransaction.Commit();
                _dbTransaction = null;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task RollbackTransaction()
        {
            try
            {
                await _lock.WaitAsync();

                if (_dbTransaction == null)
                    throw new InvalidOperationException("No transaction is running");

                _dbTransaction.Rollback();
                _dbTransaction = null;
            }
            finally
            {
                _lock.Release();
            }
        }

        public void Dispose()
        {
            _lock.Wait();

            _dbTransaction?.Rollback();
            _dbTransaction = null;
        }

        ~TransactionManager()
        {
            Dispose();
        }
    }
}

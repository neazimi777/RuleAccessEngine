using Microsoft.EntityFrameworkCore.Storage;
using RuleAccessEngine.Domain.Repositories;
using System.Transactions;

namespace RuleAccessEngine.Persistence.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRuleAccessDBContext _ruleAccessDBContext;
        private IDbContextTransaction _transaction;
        public UnitOfWork(IRuleAccessDBContext ruleAccessDBContext)
        {
            _ruleAccessDBContext = ruleAccessDBContext;
        }
        public TransactionScope TransactionScopeAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, TransactionScopeOption transactionScopeOption = TransactionScopeOption.Required)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = isolationLevel,
                Timeout = TransactionManager.MaximumTimeout
            };
            return new TransactionScope(transactionScopeOption, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _ruleAccessDBContext.SaveChangesAsync(cancellationToken);

        public void BeginTransaction()
        {
            _transaction = _ruleAccessDBContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

    }

}

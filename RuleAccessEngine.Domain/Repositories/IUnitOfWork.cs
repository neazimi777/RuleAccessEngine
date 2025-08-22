using System.Transactions;

namespace RuleAccessEngine.Domain.Repositories
{
    public interface IUnitOfWork
    {
        TransactionScope TransactionScopeAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, TransactionScopeOption transactionScopeOption = TransactionScopeOption.Required);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public void Rollback();
        public void Commit();
        public void BeginTransaction();
    }
}

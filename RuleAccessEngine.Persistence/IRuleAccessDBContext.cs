using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RuleAccessEngine.Domain;
using System.Collections.Generic;
using System.Transactions;

namespace RuleAccessEngine.Persistence
{
    public interface IRuleAccessDBContext
    {
        DbSet<Rule> Rule { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public DatabaseFacade Database { get; }
    }
}

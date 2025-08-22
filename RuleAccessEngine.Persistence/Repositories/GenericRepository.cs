using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RuleAccessEngine.Domain.Repositories;
using System.Linq.Expressions;

namespace RuleAccessEngine.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public GenericRepository(IRuleAccessDBContext dbContext) => DbContext = dbContext;

        private IRuleAccessDBContext DbContext { get; }

        public IQueryable<T> QueryAllAsNoTracking() => DbContext.Set<T>().AsNoTracking();

        public IQueryable<T> QueryByConditionAsNoTracking(Expression<Func<T, bool>> expression) => DbContext.Set<T>().Where(expression).AsNoTracking();

        public async Task<IReadOnlyList<T>> GetAllAsync() => await DbContext.Set<T>().ToListAsync();

        public async Task<T?> GetAsync(int id) => await DbContext.Set<T>().FindAsync(id);

        public async Task<bool> ExistsAsync(int id) => await GetAsync(id) != null;

        public async Task<bool> IsExist(int id) => await ExistsAsync(id);

        public T Add(T entity) => EntityOrNull(DbContext.Set<T>().Add(entity));

        private static T EntityOrNull(EntityEntry<T>? ee)
        {
            if (ee != null) return ee.Entity;
            return null!;
        }

        public T Remove(T entity) => EntityOrNull(DbContext.Set<T>().Remove(entity));

        public T Update(T entity) => EntityOrNull(DbContext.Set<T>().Update(entity));

        public T Create(T entity) => Add(entity); //DbContext.Set<T>().Add(entity);

        public T Delete(T entity) => Remove(entity); //DbContext.Set<T>().Remove(entity);

        public void AddRange(IEnumerable<T> entities) => DbContext.Set<T>().AddRange(entities);

        /// <inheritdoc cref="DbSet{TEntity}.RemoveRange(TEntity[])"/>
        public void RemoveRange(IEnumerable<T> entities) => DbContext.Set<T>().RemoveRange(entities);
    }
}


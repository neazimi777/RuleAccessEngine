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

        public async Task<T?> GetAsync(Guid id) => await DbContext.Set<T>().FindAsync(id);

        public async Task<bool> ExistsAsync(Guid id) => await GetAsync(id) != null;

        public async Task<bool> IsExist(Guid id) => await ExistsAsync(id);
        public async Task<T> CreateAsync(T entity)
        {
           var result = await DbContext.Set<T>().AddAsync(entity);
            return EntityOrNull(result);
        }
        public T Create(T entity)
        {
           return EntityOrNull(DbContext.Set<T>().Add(entity));
        }
        private static T EntityOrNull(EntityEntry<T>? ee)
        {
            if (ee != null) return ee.Entity;
            return null!;
        }

        public T Remove(T entity) => EntityOrNull(DbContext.Set<T>().Remove(entity));

        public T Update(T entity) => EntityOrNull(DbContext.Set<T>().Update(entity));

        public T Delete(T entity) => Remove(entity); 

        public void CreateRange(IEnumerable<T> entities) => DbContext.Set<T>().AddRange(entities);

        public void RemoveRange(IEnumerable<T> entities) => DbContext.Set<T>().RemoveRange(entities);
    }
}


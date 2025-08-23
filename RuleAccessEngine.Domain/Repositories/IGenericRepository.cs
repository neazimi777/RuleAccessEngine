using System.Linq.Expressions;

namespace RuleAccessEngine.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> QueryAllAsNoTracking();
        IQueryable<T> QueryByConditionAsNoTracking(Expression<Func<T, bool>> expression);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> IsExist(Guid id);
        Task<T> CreateAsync(T entity);
        T Remove(T entity);
        T Update(T entity);
        public T Create(T entity);
        public T Delete(T entity);
        public void CreateRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);

    }
}

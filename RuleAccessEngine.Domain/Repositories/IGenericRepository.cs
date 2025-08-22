using System.Linq.Expressions;

namespace RuleAccessEngine.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> QueryAllAsNoTracking();
        IQueryable<T> QueryByConditionAsNoTracking(Expression<Func<T, bool>> expression);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> IsExist(int id);
        T Add(T entity);
        T Remove(T entity);
        T Update(T entity);
        public T Create(T entity);
        public T Delete(T entity);
        public void AddRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);

    }
}

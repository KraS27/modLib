using modLib.Models.Entities;

namespace modLib.BL
{
    public interface IBaseService<T> where T : BaseModel
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<IEnumerable<T>> GetAllAsync(Predicate<T> predicate);

        public Task<T?> GetAsync(Guid id);

        public Task<T?> GetAsync(Predicate<T> predicate);

        public Task RemoveAsync(T entity);

        public Task CreateAsync(T entity);

        public Task UpdateAsync(T entity);
    }
}

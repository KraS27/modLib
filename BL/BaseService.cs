using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Models.Entities;

namespace modLib.BL
{
    public class BaseService<T> where T : BaseModel
    {
        protected readonly AppDbContext _context;

        public BaseService(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();            
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Predicate<T> predicate)
        {
            return await _context.Set<T>().Where(e => predicate(e)).ToListAsync();
        }

        public virtual async Task<T?> GetAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<T?> GetAsync(Predicate<T> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => predicate(e));
        }

        public virtual async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}

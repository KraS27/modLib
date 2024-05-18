using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities.Exceptions;
using modLib.Models.Entities;

namespace modLib.BL
{
    public abstract class BaseService<T> where T : BaseModel
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

        public virtual async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }      

        public virtual async Task RemoveAsync(int id)
        {
            var model = await GetAsync(id);         
            _context.Set<T>().Remove(model!);
            await _context.SaveChangesAsync();                       
        }

        public virtual async Task<T?> UpdateAsync(T model)
        {
            var toUpdate = await GetAsync(model.Id);

            if(toUpdate != null)
            {
                _context.Entry(toUpdate).CurrentValues.SetValues(model);

                await _context.SaveChangesAsync();
            }
            
            return toUpdate;
        }

        public virtual async Task CreateAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}

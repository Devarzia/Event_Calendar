using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventCalendar.Domain
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected EventCalendarDbContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(EventCalendarDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllEntities() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> QueryEntitiesByParameter(Expression<Func<T, bool>> predicate) => await _dbSet.AsQueryable().Where(predicate).ToListAsync();

        public async Task<T> GetEntityByID(int entityID) => await _dbSet.FindAsync(entityID);

        public async Task AddEntity(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditEntity(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntity(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventCalendar.Domain
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllEntities();
        Task<IEnumerable<T>> QueryEntitiesByParameter(Expression<Func<T, bool>> predicate);
        Task<T> GetEntityByID(int entityID);
        Task AddEntity(T entity);
        Task EditEntity(T entity);
        Task DeleteEntity(T entity);
    }
}
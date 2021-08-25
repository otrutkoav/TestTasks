using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SrmApi.Interfaces
{
    public interface IWorkEntity<T> where T:class
    {
        Task<IEnumerable<T>> GetEntitiesAsync();
        T GetEntity(int entityId);
        bool EditEntity(T entity);
        bool DeleteEntity(int entityId);
    }
}

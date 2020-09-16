using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetId(string id);
        Task<int> Create(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(string id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDB.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null);
        void Update(T item);
        void Delete(T item);
        Task Add(T item);
    }
}

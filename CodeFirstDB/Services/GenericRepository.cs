using CodeFirstDB.Data.Connection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDB.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private DbSet<T> DB;
        public GenericRepository()
        {
            context = new AppDbContext();
            DB = context.Set<T>();
        }
        public async Task Add(T item)
        {
            await DB.AddAsync(item);
        }

        public void Delete(T item)
        {
            DB.Remove(item);
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            T res = await DB.FirstOrDefaultAsync(predicate);
            return res;
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is not null)                 
                return await DB.Where(predicate).ToListAsync(); 

            return await DB.ToListAsync();
        }

        public void Update(T item)
        {
            DB.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}

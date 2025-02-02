using CRM.FileService.Application.Interfaces.Infrastructure;
using CRM.FileService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.FileService.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        private readonly PostgresDbContext dbContext;
        public GenericRepository(PostgresDbContext dbContext)
        {
            dbSet = dbContext.Set<T>();
            this.dbContext = dbContext;
        }
        public async Task<T?> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

        public async Task RemoveAsync(T entity) => await Task.Run(() => dbSet.Remove(entity));

        public async Task UpdateAsync(T entity) => await Task.Run(() => dbSet.Update(entity));
    }
}

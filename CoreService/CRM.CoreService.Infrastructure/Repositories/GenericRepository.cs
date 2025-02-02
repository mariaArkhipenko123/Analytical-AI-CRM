using CRM.CoreService.Application.Interfaces.Repositories;
using CRM.CoreService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CRM.CoreService.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        public GenericRepository(PostgresDbContext context)
        {
            dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) => await dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

        public async Task RemoveAsync(T entity) => await Task.Run(() => dbSet.Remove(entity));

        public async Task UpdateAsync(T entity) => await Task.Run(() => dbSet.Update(entity));
    }
}

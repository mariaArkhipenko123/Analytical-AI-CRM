using CRM.LoggerService.Domain.Entities;
using CRM.LoggerService.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRM.LoggerService.Infrastructure.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly LoggerDbContext _context;

        public UserLogRepository(LoggerDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(UserLog userLog)
        {
            await _context.UserLogs.AddAsync(userLog);
            await _context.SaveChangesAsync();
        }
        public async Task<UserLog> GetByIdAsync(ObjectId id)
        {
            return await _context.UserLogs.FindAsync(id);
        }

        public async Task<IEnumerable<UserLog>> GetAllAsync()
        {
            return await _context.UserLogs.ToListAsync();
        }

        public async Task<IEnumerable<UserLog>> GetByUserIdAsync(string userId)
        {
            return await _context.UserLogs.Where(log => log.UserId == userId).ToListAsync();
        }

        public async Task UpdateAsync(UserLog userLog)
        {
            _context.UserLogs.Update(userLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var userLog = await _context.UserLogs.FindAsync(id);
            if (userLog != null)
            {
                _context.UserLogs.Remove(userLog);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using CRM.LoggerService.Domain.Entities;
using CRM.LoggerService.Domain.Migrations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CRM.LoggerService.Infrastructure.Data.Context
{
    public class LoggerDbContext : DbContext
    {
        public DbSet<GraphQLLog> GraphQLLogs { get; init; }
        public DbSet<FileLog> FileLogs { get; init; }
        public DbSet<ReportLog> ReportLogs { get; init; }
        public DbSet<UserLog> UserLogs { get; init; }
        public DbSet<MigrationLog> MigrationLogs { get; init; }

        public LoggerDbContext(DbContextOptions<LoggerDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GraphQLLog>().ToCollection("GraphQLLogs");
            modelBuilder.Entity<FileLog>().ToCollection("FileLogs");
            modelBuilder.Entity<ReportLog>().ToCollection("ReportLogs");
            modelBuilder.Entity<UserLog>().ToCollection("UserLogs");
            modelBuilder.Entity<MigrationLog>().ToCollection("MigrationLogs");
        }
    }
}

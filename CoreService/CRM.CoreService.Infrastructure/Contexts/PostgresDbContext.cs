using CRM.CoreService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRM.CoreService.Infrastructure.Contexts
{
    public class PostgresDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }
        public DbSet<ReportEntity> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure DateTime properties to use 'timestamp with time zone'
            modelBuilder.Entity<UserEntity>()
                .Property(u => u.CreatedAt)
                .HasColumnType("timestamp with time zone");

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.UpdatedAt)
                .HasColumnType("timestamp with time zone");

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.ArchivedAt)
                .HasColumnType("timestamp with time zone");
        }
    }
}

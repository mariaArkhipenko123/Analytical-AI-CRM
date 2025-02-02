using CRM.ReportService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.ReportService.Infrastructure.Contexts
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }
        public DbSet<Report> Reports { get; set; }
    }
}

using CRM.FileService.Domain.Entities;
using CRM.FileService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.FileService.Infrastructure.Extensions
{
    public static class WebApplicationSeederExtension
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();
            await context.Database.MigrateAsync();

            if (!context.Files.Any())
            {
                var files = new List<FileEntity>
            {
                new FileEntity
                {
                    Path = "/files/example1.xlsx",
                    Extension = ".xlsx"
                },
                new FileEntity
                {
                    Path = "/files/example3.pdf",
                    Extension = ".pdf"
                }
            };
                foreach(var file in files)
                {
                    await context.Files.AddAsync(file);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}

using CRM.CoreService.Domain.Entities;
using CRM.CoreService.Infrastructure.Contexts;
using CRM.CoreService.Infrastructure.Extensions.Redis;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Text;
using DistributedCacheExtensions = CRM.CoreService.Infrastructure.Extensions.Redis.DistributedCacheExtensions;

namespace CRM.CoreService.Infrastructure.Extensions
{
    public static class WebApplicationSeederExtension
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<UserEntity>>();
            var roleManager = services.GetRequiredService<RoleManager<RoleEntity>>();
            var dbContext = services.GetRequiredService<PostgresDbContext>();
            var cache = services.GetRequiredService<IDistributedCache>();

            await dbContext.Database.MigrateAsync();

            var builder = new StringBuilder();
            var entityTypes = dbContext.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                var tableName = entityType.GetTableName();
                var schema = entityType.GetSchema() ?? "dbo";
                builder.AppendLine($"Schema: {schema}, Table: {tableName}");
                var properties = entityType.GetProperties();
                foreach (var property in properties)
                {
                    var columnName = property.GetColumnName();
                    var propertyType = property.ClrType.Name;
                    builder.AppendLine($"\tColumn: {columnName}, Type: {propertyType}");
                }
            }


            var dbSchema = builder.ToString();
            await DistributedCacheExtensions.SetAsync<string>(cache, "db_schema", dbSchema, (options)=>options.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1));

            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new RoleEntity { Name = role });
                }
            }

            var userRole = await roleManager.FindByNameAsync("User");
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (userRole != null && adminRole != null)
            {
                var userRoleClaims = await roleManager.GetClaimsAsync(userRole);
                var adminRoleClaims = await roleManager.GetClaimsAsync(adminRole);
                if (!userRoleClaims.Any(c => c.Type == "Permission" && c.Value == "UserTestValue") && 
                    !adminRoleClaims.Any(c => c.Type == "Permission" && c.Value == "AdminTestValue"))
                {
                    await roleManager.AddClaimAsync(userRole, new Claim("Permission", "UserTestValue"));
                    await roleManager.AddClaimAsync(adminRole, new Claim("Permission", "AdminTestValue"));
                }
            }

            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";
            var userEmail = "user@example.com";
            var userPassword = "User@123";

            if (await userManager.FindByEmailAsync(adminEmail) == null && await userManager.FindByEmailAsync(userEmail) == null)
            {
                var admin = new UserEntity
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var user = new UserEntity
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };

                var result1 = await userManager.CreateAsync(admin, adminPassword);
                var result2 = await userManager.CreateAsync(user, userPassword);
                if (result1.Succeeded && result2.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}

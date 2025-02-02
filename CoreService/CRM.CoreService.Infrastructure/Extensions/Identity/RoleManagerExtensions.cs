using CRM.CoreService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Infrastructure.Extensions.Identity
{
    public static class RoleManagerExtensions
    {
        public static async Task AddClaimToRoleAsync(this RoleManager<RoleEntity> roleManager, string roleName, Claim claim)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await roleManager.AddClaimAsync(role, claim);
            }
        }
        public static async Task RemoveClaimFromRoleAsync(this RoleManager<RoleEntity> roleManager, string roleName, Claim claim)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await roleManager.RemoveClaimAsync(role, claim);
            }
        }
    }

}

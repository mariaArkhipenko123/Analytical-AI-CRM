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
    public static class UserManagerExtensions
    {
        public static async Task<IEnumerable<Claim>> GetRoleClaimsForUserAsync(
            this UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager,
            UserEntity user)
        {
            var claims = new List<Claim>();
            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var roleEntity = await roleManager.FindByNameAsync(role);
                if (roleEntity != null)
                {
                    var roleClaims = await roleManager.GetClaimsAsync(roleEntity);
                    claims.AddRange(roleClaims);
                }
            }
            return claims;
        }
    }

}

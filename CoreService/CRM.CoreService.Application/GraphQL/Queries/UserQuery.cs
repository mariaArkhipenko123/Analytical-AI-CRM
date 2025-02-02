using AutoMapper;
using HotChocolate;
using CRM.CoreService.Application.Interfaces.Repositories;
using CRM.CoreService.Application.Models.DTOs;
using CRM.CoreService.Infrastructure.Services;

namespace CRM.CoreService.Application.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class UserQuery
    {
        public async Task<UserDTO?> GetUserAsync(
            [Service] IUnitOfWork uow,
            [Service] IMapper mapper,
            [Service] ICacheService cache,
            Guid id)
        {
            var cacheKey = $"GetUserById:{id}";

            var cachedUser = await cache.GetAsync<UserDTO>(cacheKey);
            if (cachedUser != null)
                return cachedUser;

            var user = await uow.UserRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            var userDto = mapper.Map<UserDTO>(user);

            await cache.SetAsync(
                cacheKey,
                userDto,
                TimeSpan.FromMinutes(10), 
                TimeSpan.FromHours(1)     
            );

            return userDto;
        }
        public async Task<IEnumerable<UserDTO>> GetUsersAsync(
            [Service] IUnitOfWork uow,
            [Service] IMapper mapper,
            [Service] ICacheService cache)
        {
            const string cacheKey = "GetAllUsers";

            var cachedUsers = await cache.GetAsync<IEnumerable<UserDTO>>(cacheKey);
            if (cachedUsers != null)
                return cachedUsers;

            var users = await uow.UserRepository.GetAllAsync();
            var userDtos = mapper.Map<IEnumerable<UserDTO>>(users);

            await cache.SetAsync(
                cacheKey,
                userDtos,
                TimeSpan.FromMinutes(10), 
                TimeSpan.FromHours(1)  
            );

            return userDtos;
        }
    }
}

using AutoMapper;
using CRM.CoreService.Application.Interfaces.Repositories;
using CRM.CoreService.Application.Models.DTOs;
using CRM.CoreService.Application.Models.Inputs;
using CRM.CoreService.Domain.Entities;
using CRM.CoreService.Domain.Enums;
using CRM.CoreService.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

namespace CRM.CoreService.Application.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class UserMutation
    {
        public async Task<UserDTO> CreateUserAsync(
            [Service] IUnitOfWork uow,
            [Service] IMapper mapper,
            [Service] ICacheService cache,  
            CreateUserInput input)
        {
            var user = new UserEntity
            {
                UserName = input.UserName,
                Email = input.Email,
                Status = Enum.Parse<UserStatus>(input.Status),
                CreatedAt = DateTime.UtcNow
            };

            var passwordHasher = new PasswordHasher<UserEntity>();
            user.PasswordHash = passwordHasher.HashPassword(user, input.Password);

            await uow.UserRepository.AddAsync(user);
            await uow.SaveChangesAsync();

            var userDto = mapper.Map<UserDTO>(user);
            var cacheKey = $"GetUserById:{user.Id}";
            await cache.SetAsync(
                cacheKey,
                userDto,
                TimeSpan.FromMinutes(10),  
                TimeSpan.FromHours(1)     
            );

            return userDto;
        }

        public async Task<UserDTO> UpdateUserAsync(
            [Service] IUnitOfWork uow,
            [Service] IMapper mapper,
            [Service] ICacheService cache, 
            UpdateUserInput input)
        {
            var user = await uow.UserRepository.GetByIdAsync(input.Id);
            if (user == null)
            {
                throw new GraphQLException("User not found");
            }

            if (!string.IsNullOrEmpty(input.UserName))
                user.UserName = input.UserName;
            if (!string.IsNullOrEmpty(input.Email))
                user.Email = input.Email;
            if (!string.IsNullOrEmpty(input.Password))
                user.PasswordHash = input.Password;
            if (!string.IsNullOrEmpty(input.Status))
                user.Status = Enum.Parse<UserStatus>(input.Status);

            user.UpdatedAt = DateTime.UtcNow;

            await uow.SaveChangesAsync();

            var updatedUserDto = mapper.Map<UserDTO>(user);
            var cacheKey = $"GetUserById:{user.Id}";
            await cache.SetAsync(
                cacheKey,
                updatedUserDto,
                TimeSpan.FromMinutes(10), 
                TimeSpan.FromHours(1)    
            );

            return updatedUserDto;
        }

        public async Task<bool> DeleteUserAsync(
          [Service] IUnitOfWork uow,
          [Service] ICacheService cacheService,
          Guid id)
        {
            var user = await uow.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new GraphQLException("User not found");
            }

            await uow.UserRepository.RemoveAsync(user);
            await uow.SaveChangesAsync();

            string cacheKey = $"User:{id}";
            await cacheService.RemoveAsync(cacheKey);
            return true;
        }
    }
}

using AutoMapper;
using CRM.CoreService.Application.Interfaces.Repositories;
using CRM.CoreService.Application.Models.DTOs;
using CRM.CoreService.Application.Models.Inputs;
using CRM.CoreService.Domain.Entities;
using CRM.CoreService.Domain.Enums;
using CRM.CoreService.Infrastructure.Services;

namespace CRM.CoreService.Application.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class ReportMutation
    {
        public async Task<ReportDTO> CreateReportAsync(
             [Service] IUnitOfWork uow,
             [Service] IMapper mapper,
             [Service] ICacheService cache,
             CreateReportInput input)
        {
            var report = new ReportEntity
            {
                Type = input.Type,
                Status = Enum.Parse<ReportStatus>(input.Status),
                UserId = input.UserId,
                FileId = input.FileId
            };

            await uow.ReportRepository.AddAsync(report);
            await uow.SaveChangesAsync();

            var reportDto = mapper.Map<ReportDTO>(report);
            var cacheKey = $"GetReportById:{report.Id}"; 
            await cache.SetAsync(cacheKey, reportDto, TimeSpan.FromMinutes(10), TimeSpan.FromHours(1));

            return reportDto;
        }
        public async Task<ReportDTO> UpdateReportAsync(
            [Service] IUnitOfWork uow,
            [Service] IMapper mapper,
            [Service] ICacheService cache,
            UpdateReportInput input)
        {
            var report = await uow.ReportRepository.GetByIdAsync(input.Id);
            if (report == null)
                throw new GraphQLException("Report not found");

            if (!string.IsNullOrEmpty(input.Type))
                report.Type = input.Type;
            if (!string.IsNullOrEmpty(input.Status))
                report.Status = Enum.Parse<ReportStatus>(input.Status);

            await uow.SaveChangesAsync();

            var updatedReportDto = mapper.Map<ReportDTO>(report);
            var cacheKey = $"GetReportById:{report.Id}"; 
            await cache.SetAsync(cacheKey, updatedReportDto, TimeSpan.FromMinutes(10), TimeSpan.FromHours(1));

            return updatedReportDto;
        }
        public async Task<bool> DeleteReportAsync(
            [Service] IUnitOfWork uow,
            [Service] ICacheService cache,
            Guid id)
        {
            var report = await uow.ReportRepository.GetByIdAsync(id);
            if (report == null)
                throw new GraphQLException("Report not found");

            await uow.ReportRepository.RemoveAsync(report);
            await uow.SaveChangesAsync();

            string cacheKey = $"Report:{id}";
            await cache.RemoveAsync(cacheKey);


            return true;
        }
    }
}

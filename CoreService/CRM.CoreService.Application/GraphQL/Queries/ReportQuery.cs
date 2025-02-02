using AutoMapper;
using HotChocolate;
using CRM.CoreService.Application.Interfaces.Repositories;
using CRM.CoreService.Application.Models.DTOs;
using CRM.CoreService.Infrastructure.Services;

namespace CRM.CoreService.Application.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class ReportQuery
    {
        public async Task<ReportDTO?> ReportAsync(
            [Service] IUnitOfWork uow,
            [Service] IMapper mapper,
            [Service] ICacheService cache,  
            Guid id)
        {
            var cacheKey = $"GetReportById:{id}";  

            var cachedReport = await cache.GetAsync<ReportDTO>(cacheKey);
            if (cachedReport != null)
                return cachedReport;  

            var report = await uow.ReportRepository.GetByIdAsync(id);
            if (report == null)
                return null;

            var reportDto = mapper.Map<ReportDTO>(report);

            await cache.SetAsync(
                cacheKey,
                reportDto,
                TimeSpan.FromMinutes(10), 
                TimeSpan.FromHours(1)      
            );

            return reportDto;
        }

        public async Task<IEnumerable<ReportDTO>> ReportsAsync(
            [Service] IUnitOfWork uow,
            [Service] IMapper mapper,
            [Service] ICacheService cache)  
        {
            const string cacheKey = "GetAllReports";  

            var cachedReports = await cache.GetAsync<IEnumerable<ReportDTO>>(cacheKey);
            if (cachedReports != null)
                return cachedReports;  

            var reports = await uow.ReportRepository.GetAllAsync();
            var reportDtos = mapper.Map<IEnumerable<ReportDTO>>(reports);

            await cache.SetAsync(
                cacheKey,
                reportDtos,
                TimeSpan.FromMinutes(10), 
                TimeSpan.FromHours(1)      
            );

            return reportDtos;
        }
    }
}

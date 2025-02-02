using AutoMapper;
using CRM.CoreService.Application.Models.DTOs;
using CRM.CoreService.Domain.Entities;

namespace CRM.CoreService.Application.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())) 
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))                  
            .ForMember(dest => dest.Reports, opt => opt.MapFrom(src => src.Reports));

            CreateMap<ReportEntity, ReportDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); 
        }
    }
}

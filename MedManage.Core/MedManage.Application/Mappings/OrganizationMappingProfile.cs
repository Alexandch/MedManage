using AutoMapper;
using MedManage.Application.DTOs;
using MedManage.Domain.Entities;


namespace MedManage.Application.Mappings
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<Organization, OrganizationDTO>();
            
            CreateMap<OrganizationDTO, Organization>();
        }
    }
}


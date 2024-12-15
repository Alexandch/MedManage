using AutoMapper;
using MedManage.Application.DTOs; 
using MedManage.Domain.Entities;
using Announcement = MedManage.Domain.Entities.Announcement; 

namespace MedManage.Application.Mappings
{
    public class AnnouncementMappingProfile : Profile
    {
        public AnnouncementMappingProfile()
        {
            CreateMap<Announcement, AnnouncementDTO>();
            CreateMap<AnnouncementDTO, Announcement>();
        }
    }
}
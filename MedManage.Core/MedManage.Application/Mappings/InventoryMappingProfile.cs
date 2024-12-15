using AutoMapper;
using MedManage.Application.DTOs;
using MedManage.Domain.Entities;

namespace MedManage.Application.Mappings
{
    public class InventoryMappingProfile: Profile
    {
        public InventoryMappingProfile()
        {
            CreateMap<Inventory, InventoryDTO>();
            
            CreateMap<InventoryDTO, Inventory>();
        }
    }  
}
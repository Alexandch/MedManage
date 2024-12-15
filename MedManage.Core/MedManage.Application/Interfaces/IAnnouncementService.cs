using MedManage.Application.DTOs;
using MedManage.Domain.Entities;
using MedManage.Domain.Enums;

namespace MedManage.Application.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDTO>> GetAllAnnouncementsAsync();
        Task<AnnouncementDTO> GetAnnouncementByIdAsync(Guid announcementId);
        
        Task<IEnumerable<AnnouncementDTO>> GetAllAnnouncementsPaginatedAsync(
            int pageNumber, 
            int pageSize, 
            TypeOfSort sortBy, 
            string searchFilter, 
            ProductType productType,
            InventoryStatus statusInventory);

        Task CreateNewAnnouncementAsync(AnnouncementDTO announcementRequest, Guid userId);
        Task ChangeAnnouncementContentAsync(Guid announcementId, string content);
        Task DeleteAnnouncementAsync(Guid announcementId);

        string GetUserNameFromToken();
    }
}
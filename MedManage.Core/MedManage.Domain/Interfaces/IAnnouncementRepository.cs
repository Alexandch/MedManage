using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedManage.Domain.Entities;
using MedManage.Domain.Enums;

namespace MedManage.Domain.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAsync();

        Task<Announcement> GetByIdAsync(Guid announcementId);

        IQueryable<Announcement> GetPaginated(
            int pageNumber,
            int pageSize,
            string searchFilter,
            ProductType productType,
            InventoryStatus inventoryStatus);

        Task CreateAsync(Announcement announcement);

        Task UpdateAsync(Announcement announcement);

        Task DeleteAsync(Announcement announcement);

        Task<IEnumerable<Announcement>> GetAnnouncementsByAuthorAsync(string authorName);

        Task<IEnumerable<Announcement>> GetAnnouncementsByDateAsync(DateTime date);

        Task<IEnumerable<Announcement>> SearchAnnouncementsByContentAsync(string content);
    }
}
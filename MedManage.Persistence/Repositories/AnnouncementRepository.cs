using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedManage.Domain.Entities;
using MedManage.Domain.Enums;
using MedManage.Domain.Interfaces;
using MedManage.Persistence.Data;

namespace MedManage.Persistence.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AnnouncementDbContext _context;

        public AnnouncementRepository(AnnouncementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _context.Announcements
                .Include(a => a.CreatedByUser)
                .OrderBy(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<Announcement> GetByIdAsync(Guid announcementId)
        {
            return await _context.Announcements
                .Include(a => a.CreatedByUser)
                .FirstOrDefaultAsync(a => a.AnnouncementId == announcementId);
        }

        public IQueryable<Announcement> GetPaginated(
            int pageNumber,
            int pageSize,
            string searchFilter,
            ProductType productType,
            InventoryStatus inventoryStatus)
        {
            var announcements = _context.Announcements
                .Include(a => a.CreatedByUser)
                .AsQueryable();

            // Фильтрация по продукту
            if (productType != ProductType.All) // Сравниваем с действительным значением перечисления
            {
                announcements = announcements.Where(a => a.TypeProduct == productType);
            }

            // Фильтрация по статусу инвентаря
            if (inventoryStatus != InventoryStatus.All) // Сравниваем с действительным значением перечисления
            {
                announcements = announcements.Where(a => a.StatusInventory == inventoryStatus);
            }

            // Фильтрация по тексту
            if (!string.IsNullOrWhiteSpace(searchFilter))
            {
                announcements = announcements.Where(a => a.Title.Contains(searchFilter) || a.Content.Contains(searchFilter));
            }

            // Сортировка по умолчанию
            announcements = announcements.OrderByDescending(a => a.CreatedAt);

            // Пагинация
            return announcements.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }


        public async Task CreateAsync(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Announcement announcement)
        {
            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsByAuthorAsync(string authorName)
        {
            return await _context.Announcements
                .Where(a => a.CreatedByUser.FullName.Contains(authorName))
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsByDateAsync(DateTime date)
        {
            return await _context.Announcements
                .Where(a => a.CreatedAt.Date == date.Date)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> SearchAnnouncementsByContentAsync(string content)
        {
            return await _context.Announcements
                .Where(a => a.Content.Contains(content))
                .ToListAsync();
        }
    }
}

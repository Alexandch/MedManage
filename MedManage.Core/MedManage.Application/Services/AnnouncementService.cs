using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using MedManage.Application.DTOs;
using MedManage.Application.Interfaces;
using MedManage.Domain.Entities;
using MedManage.Domain.Interfaces;
using MedManage.Domain.Enums;

namespace MedManage.Application.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;


        public AnnouncementService(
            IMapper mapper,
            IAnnouncementRepository announcementRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _announcementRepository = announcementRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        
        public async Task<IEnumerable<AnnouncementDTO>> GetAllAnnouncementsAsync()
        {
            var announcements = await _announcementRepository.GetAllAsync();


            return _mapper.Map<IEnumerable<AnnouncementDTO>>(announcements);
        }

        public async Task<AnnouncementDTO> GetAnnouncementByIdAsync(Guid announcementId)
        {
            var announcement = await _announcementRepository.GetByIdAsync(announcementId);


            return _mapper.Map<AnnouncementDTO>(announcement);
        }

        public async Task<IEnumerable<AnnouncementDTO>> GetAllAnnouncementsPaginatedAsync(
            int pageNumber, 
            int pageSize, 
            TypeOfSort sortBy, 
            string searchFilter, 
            ProductType productType,
            InventoryStatus statusInventory)
        {
            // validate

            var announcements = _announcementRepository.GetPaginated(
                pageNumber, 
                pageSize, 
                searchFilter, 
                productType, 
                statusInventory);

            // Apply projection at the service level
            var projectedAnnouncements = announcements.ProjectTo<AnnouncementDTO>(_mapper.ConfigurationProvider);
    
            return await projectedAnnouncements.ToListAsync();
        }

        public async Task CreateNewAnnouncementAsync(AnnouncementDTO announcementRequest, Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("Пользователь с данным userId не найден.");
            }

            Announcement announcement = _mapper.Map<Announcement>(announcementRequest);
            announcement.Title = announcement.Title;

            await _announcementRepository.CreateAsync(announcement);
        }

        public async Task ChangeAnnouncementContentAsync(Guid announcementId, string content)
        {
            var announcement = await _announcementRepository.GetByIdAsync(announcementId);


            announcement.Content = content;
            announcement.UpdatedAt = DateTime.UtcNow;

            await _announcementRepository.UpdateAsync(announcement);
        }

        public async Task DeleteAnnouncementAsync(Guid announcementId)
        {
            var announcement = await _announcementRepository.GetByIdAsync(announcementId);
            if (announcement == null) Console.WriteLine("Объявелнеие не существует");
            await _announcementRepository.DeleteAsync(announcement);
        }

        public string GetUserNameFromToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User?.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException("Пользователь не аутентифицирован.");
            }

            var token = httpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("Authorization token is missing.");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "name");
            if (nameClaim == null)
            {
                throw new InvalidOperationException("В payload name не найдено.");
            }

            // Возвращаем имя пользователя
            return nameClaim.Value;
        }
    }
}

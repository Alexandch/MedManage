using MedManage.Application.DTOs;
using MedManage.Application.Interfaces;
using MedManage.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MedManage.Application.Filters;

namespace MedManage.WebAPI.Controllers
{
    [ApiController]
    [ValidateModelState] 
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        // Получить все объявления
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAnnouncementsAsync()
        {
            var announcements = await _announcementService.GetAllAnnouncementsAsync();
            return Ok(announcements);
        }

        // Получить объявление по ID
        [HttpGet("{announcementId}")]
        public async Task<IActionResult> GetAnnouncementByIdAsync(Guid announcementId)
        {
            var announcement = await _announcementService.GetAnnouncementByIdAsync(announcementId);
            if (announcement == null)
            {
                return NotFound(new { message = "Announcement not found." });
            }
            return Ok(announcement);
        }

        // Получить все объявления с пагинацией
        [HttpGet("paginated")]
        public async Task<IActionResult> GetAllAnnouncementsPaginatedAsync(
            int pageNumber, 
            int pageSize, 
            TypeOfSort sortBy, 
            string searchFilter, 
            ProductType productType,
            InventoryStatus statusInventory)
        {
            var announcements = await _announcementService.GetAllAnnouncementsPaginatedAsync(
                pageNumber, 
                pageSize, 
                sortBy, 
                searchFilter, 
                productType, 
                statusInventory
            );
            return Ok(announcements);
        }

        // Создать новое объявление
        [HttpPost("create")]
        public async Task<IActionResult> CreateNewAnnouncementAsync([FromBody] AnnouncementDTO announcementRequest)
        {
            //var userId = Guid.Parse(User.Identity.Name); // Предполагается, что в claims есть идентификатор пользователя
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized("Invalid UserId");
            }
            
            await _announcementService.CreateNewAnnouncementAsync(announcementRequest, userId);
            return CreatedAtAction(nameof(GetAnnouncementByIdAsync), new { announcementId = announcementRequest.AnnouncementId }, announcementRequest);
        }

        // Изменить содержание объявления
        [HttpPut("{announcementId}")]
        public async Task<IActionResult> ChangeAnnouncementContentAsync(Guid announcementId, [FromBody] string content)
        {
            await _announcementService.ChangeAnnouncementContentAsync(announcementId, content);
            return NoContent();
        }

        // Удалить объявление
        [HttpDelete("{announcementId}")]
        public async Task<IActionResult> DeleteAnnouncementAsync(Guid announcementId)
        {
            await _announcementService.DeleteAnnouncementAsync(announcementId);
            return NoContent();
        }
    }
}

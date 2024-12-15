

using MedManage.Domain.Enums;

namespace MedManage.Domain.Entities;

public class Announcement
{
    public Guid AnnouncementId { get; set; } 
    public string Title { get; set; } 
    public string Content { get; set; } 
    public DateTime CreatedAt { get; set; } 
    public DateTime? ExpirationDate { get; set; } 
    public Guid CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; }

    public InventoryStatus StatusInventory { get; set; }
    public ProductType TypeProduct { get; set; }
    public DateTime UpdatedAt { get; set; }
}




﻿
namespace MedManage.Domain.Entities;


public class Organization
{
    public Guid OrganizationId { get; set; } 
    public string Name { get; set; } 
    public string Address { get; set; }
        
    // Поля для контакта
    public string PhoneNumber { get; set; } 
    public string Email { get; set; }

    public DateTime CreatedAt { get; set; } 
}


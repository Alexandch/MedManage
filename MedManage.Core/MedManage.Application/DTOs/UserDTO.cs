using MedManage.Domain.Entities;
using MedManage.Domain.Enums;

namespace MedManage.Application.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public Organization PhoneNumber { get; set; } // Общий контактный класс
    public Organization Email { get; set; }
    public Permissions Permissions{ get; set; }
}

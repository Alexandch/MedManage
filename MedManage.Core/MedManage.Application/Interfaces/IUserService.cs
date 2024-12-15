using MedManage.Application.DTOs;
using MedManage.Domain.Entities;
using MedManage.Domain.Enums;

namespace MedManage.Application.Interfaces
{
    public interface IUserService
    {

        Task<IEnumerable<UserDTO>> GetAllUsersExceptAsync(Guid userId);
        
        Task UpdateUserInfoAsync( UserDTO updatedUser);
        
        public string GetUserNameFromToken();
    }
}

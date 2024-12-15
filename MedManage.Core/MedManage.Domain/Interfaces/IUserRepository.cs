using MedManage.Domain.Entities;

namespace MedManage.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        
        Task<User> GetByIdAsync(Guid userId);
        
        Task AddAsync(User user);
        
        Task UpdateAsync(User user);
        
    }
}
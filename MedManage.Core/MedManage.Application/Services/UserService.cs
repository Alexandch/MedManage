using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MedManage.Application.DTOs;
using MedManage.Application.Interfaces;
using MedManage.Domain.Entities;
using MedManage.Domain.Interfaces;

namespace MedManage.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        
        
        public async Task<IEnumerable<UserDTO>> GetAllUsersExceptAsync(Guid userId)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var filteredUsers = users.Where(u => u.UserId != userId);
            return _mapper.Map<IEnumerable<UserDTO>>(filteredUsers);
        }
        
        public async Task UpdateUserInfoAsync(UserDTO updatedUser)
        {
            if (updatedUser == null) throw new ArgumentNullException(nameof(updatedUser));

            var existingUser = await _userRepository.GetByIdAsync(updatedUser.Id);
            if (existingUser == null) throw new InvalidOperationException("User not found.");

            _mapper.Map(updatedUser, existingUser);
            await _userRepository.UpdateAsync(existingUser);
        }
        
        public string GetUserNameFromToken()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Token is missing.");

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            return jwtToken?.Claims.FirstOrDefault(c => c.Type == "name")?.Value 
                   ?? throw new InvalidOperationException("Username claim not found in token.");
        }
    }
}

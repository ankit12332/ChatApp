using ChatApp.Dtos;
using ChatApp.Models;

namespace ChatApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}

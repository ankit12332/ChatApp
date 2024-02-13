using ChatApp.Data;
using ChatApp.Dtos;
using ChatApp.Models;
using ChatApp.Repositories.Interfaces;
using ChatApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> CreateUserAsync(User user)
        {
            if (user.Password == null)
            {
                throw new ArgumentNullException(nameof(user.Password), "Password cannot be null.");
            }

            user.Password = PasswordHelper.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                CreatedAt = user.CreatedAt
            };
        }


        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await _context.Users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                CreatedAt = user.CreatedAt
            }).ToListAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Where(user => user.Id == id)
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    CreatedAt = user.CreatedAt
                }).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new KeyNotFoundException($"A user with the ID {id} was not found.");
            }

            return user;
        }


        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

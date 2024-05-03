using infoManagerAPI.Data;
using infoManagerAPI.DTO.User.Response;
using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace infoManagerAPI.Database.Repositories
{
    public class UsersRepository (InfoManagerDbContext _context) : IUsersRepository
    {
        public async Task<bool> CreateAsync(User user)
        {
            await _context.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePasswordAsync(string password, User user)
        {
            _context.Users.Update(user);
            Detach(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public void Detach(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            EntityEntry<User> entry = _context.Entry(user);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}

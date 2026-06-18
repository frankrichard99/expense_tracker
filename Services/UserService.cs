using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
           
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Expenses)
                .ThenInclude(e => e.Category)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        /*
        public async Task<User> AddUser(string firstName, string lastName, string email, int age)
        {
            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Age = age
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

      */
        public async Task<User?> UpdateUser(int? id, string? firstName, string? lastName, string? email, int? age)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null) return null;

           if(firstName != null)
            {
                existingUser.FirstName = firstName;
            }
            if (lastName != null)
            {
                existingUser.LastName = lastName;
            }
            if (email != null)
            {
                existingUser.Email = email;
            }
            if (age.HasValue)
            {
                existingUser.Age = age.Value;
            }
           
           await _context.SaveChangesAsync();
            return existingUser;
        }

    
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
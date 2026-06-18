using ExpenseTracker.Data;
using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;
        public AuthService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<User?> Register(RegisterDto dto) {
            bool emailExists = await _context.Users.AnyAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (emailExists)
            {
                return null;
            }
            
            var hasher = new PasswordHasher<User>();
            string hashedPassword = hasher.HashPassword(null!, dto.Password);
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Age = dto.Age,
                PasswordHash = hashedPassword,
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;


        }

        public async Task<string?> Login(LoginDto dto) {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (user == null) { 
            return null;
            }
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null; 
            }
            return _tokenService.CreateToken(user);
        }
    }
}

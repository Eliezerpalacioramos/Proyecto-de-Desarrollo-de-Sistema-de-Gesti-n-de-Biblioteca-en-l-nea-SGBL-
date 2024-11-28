using SGBL.Business.Interfaces;
using SGBL.Data.Contexts;
using System;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBL.Data.Models;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace SGBL.Business.Services
{
    public class UserService: IUserService
    {
        private readonly LibraryDbContext _context;

        public UserService(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> RegisterUser(User user)
        {
            if (await IsEmailTaken(user.Email))
            {
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); 
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}

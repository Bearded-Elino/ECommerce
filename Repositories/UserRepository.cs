using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public UserRepository(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                Console.WriteLine("password hashed successfully");
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                string subject = "Welcome to Vale shop";
                string body = $"Dear {user.FirstName}, \n\nThank you for registering with us";
                await _emailService.SendEmailAsync(user.Email, subject, body);
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine("error: user not registered");
                throw new Exception("User could not be registered", e);
            }
        }

        public async Task<User> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginViewModel.Email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.Password))
                {
                    Console.WriteLine("user not found or incorrect password");
                    throw new Exception("email/password incorrect");
                }

                string subject = "Login notification";
                string body = $"Dear {user.FirstName}, \n\nA login activity just occurred on your account. If this was not you, please contact us now";
                await _emailService.SendEmailAsync(user.Email, subject, body);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during login: " + e.Message);
                throw new Exception("login failed", e);
            }
        }

    }
}
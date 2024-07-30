using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        { 
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                Console.WriteLine("password hashed successfully");
                await _context.Users.AddAsync(user);
                Console.WriteLine("user created successfully");
                await _context.SaveChangesAsync();
                Console.WriteLine("user has been saved");
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
                Console.WriteLine("found the user");
                if (user== null || !BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.Password))
                {
                    Console.WriteLine("user not found");
                    
                    throw new Exception("email/password incorrect");
                }

                return user;

            }
            catch (Exception e)
            {
                Console.WriteLine("user not found");
                throw new Exception("login failed", e);
            }
            
        }
    }
}
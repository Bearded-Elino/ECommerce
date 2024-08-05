using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;

namespace ValeShop.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        private readonly ISession _session;

        public CartRepository(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _context = context;
        }

        public async Task AddToCart(Guid productId, int quantity)
        {
            var sessionId = _session.GetString("sessionId");
            var userId = _session.GetString("userId");

            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.ProductId == productId && c.SessionId == sessionId && c.UserId == Guid.Parse(userId));

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                _context.Carts.Update(existingCartItem);
            }
            else
            {
                var cartItem = new Cart
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Quantity = quantity,
                    SessionId = sessionId,
                    UserId = Guid.Parse(userId), // Set the UserId from session
                    CreatedDate = DateTime.Now
                };
                _context.Carts.Add(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCart(Guid productId)
        {
            var existingCartItem = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == productId);
            Console.WriteLine($"item with ID {productId} found");
            if (existingCartItem != null)
            {
                _context.Carts.Remove(existingCartItem);
                await _context.SaveChangesAsync();
                Console.WriteLine($"item with ID {productId} has been deleted from the database");

            }
            else
            {
                Console.WriteLine($"item with ID {productId} not found");
            }

        }

        public async Task<List<Cart>> GetCartItems()
        {
            throw new NotImplementedException();
        }

        public async Task ClearCart()
        {
            throw new NotImplementedException();

        }
    }
}

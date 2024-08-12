using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;

namespace ValeShop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)  
                .ThenInclude(od => od.Product)  
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

    }
}
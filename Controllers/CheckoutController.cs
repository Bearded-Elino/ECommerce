using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly AppDbContext _context;

        public CheckoutController(ICountryRepository countryRepository,AppDbContext context , IOrderRepository orderRepository)
        {
            _countryRepository = countryRepository;
            _orderRepository = orderRepository;
            _context = context;
        }

        public async Task<IActionResult> Checkout(Guid orderId)
        {
            
            var sessionId = HttpContext.Session.GetString("sessionId");
            var cartItems = _context.Carts
                .Include(c => c.Product)
                .Where(c => c.SessionId == sessionId)
                .Select(c => new CartViewModel()
                {
                    Name = c.Product.Name ?? "",
                    ImageUrl = c.Product.ImageUrl ?? "",
                    Quantity = c.Quantity,
                    ProductId = c.ProductId,
                    Price = c.Product.Price ,
                    Total = c.Product.Price * c.Quantity
                }).ToList();
            
            var cartItemViewModel = new CartItemViewModel
            {
                CartItems = cartItems
                
            };

            return View(cartItemViewModel);

        }


        private async Task<Order> GetOrder(Guid orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

    }
}
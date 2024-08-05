using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.ViewModels;

namespace ValeShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly AppDbContext _context;

        public CartController(ICartRepository cartRepository, AppDbContext context)
        {
            _cartRepository = cartRepository;
            _context = context;
        }

        // GET
        [HttpGet]
        public IActionResult Cart()
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
                    Price = c.Product.Price ,
                    Total = c.Product.Price * c.Quantity
                }).ToList();
            
            var cartItemViewModel = new CartItemViewModel
            {
                CartItems = cartItems
            };

            return View(cartItemViewModel);

        }


        
        /*[HttpGet]
        public IActionResult MiniCart()
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
                    Price = c.Product.Price,
                    Total = c.Product.Price * c.Quantity,
                }).ToList();

            var cartItemViewModel = new CartItemViewModel
            {
                CartItems = cartItems
            };

            // Pass cartItemViewModel to the PartialView
            return PartialView("_MiniCart", cartItemViewModel);
        }*/

        [HttpPost]

        public async Task<IActionResult> AddToCartNow(Guid productId, int quantity)
        {
            if (HttpContext.Session.GetString("sessionId") == null)
            {
                TempData["LoginToAddToCart"] = "log into your account to add to cart and process your orders!";
                

                return RedirectToAction("Login", "User");
            }
            await _cartRepository.AddToCart(productId, quantity);
            return RedirectToAction("Cart", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItemFromCart(Guid productId)
        {
            try
            {
                //var testProduct = Guid.Parse("2112c8c1-57b1-4425-b57b-87d29fd06b96")
                
                await _cartRepository.RemoveFromCart(productId);
                return Content("item removed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("cannot remove product from cart");
                throw new Exception(ex.Message);
            }
        }


    }
}
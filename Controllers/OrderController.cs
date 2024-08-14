using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValeShop.interfaces;
using ValeShop.ViewModels;
using ValeShop.Models;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;


namespace ValeShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly AppDbContext _context;

        public OrderController(ICountryRepository countryRepository, AppDbContext context)
        {
            _countryRepository = countryRepository;
            _context = context;
        }

        public async Task<IActionResult> Order()
        {
            var countries = await _countryRepository.GetAllCountriesAsync();
            var sortedCountries = countries.OrderBy(c => c.Name).ToList();

            var model = new CheckoutViewModel
            {
                Countries = sortedCountries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> OrderById(Guid orderId)
        {
            try
            {
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null)
                {
                    return NotFound(new { status = "failed", message = "Order not found" });
                }

                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest(new { status = "failed", message = "Could not get order" });
            }
        }


        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}

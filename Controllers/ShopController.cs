using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        // GET
        [HttpGet]
        public IActionResult ShopList()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        
    }
}
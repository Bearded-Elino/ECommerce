using Microsoft.AspNetCore.Mvc;

namespace ValeShop.Controllers
{
    public class OrderController : Controller
    {
        // GET
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
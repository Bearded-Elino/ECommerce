using Microsoft.AspNetCore.Mvc;

namespace ValeShop.Controllers
{
    public class CheckoutController : Controller
    {
        // GET
        public IActionResult Checkout()
        {
            return View();
        }
        
        
    }
}
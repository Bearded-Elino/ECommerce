using Microsoft.AspNetCore.Mvc;

namespace ValeShop.Controllers
{
    public class CartController : Controller
    {
        // GET
        public IActionResult Cart()
        {
            return View();
        }
    }
}
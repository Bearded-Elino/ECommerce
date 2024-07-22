using Microsoft.AspNetCore.Mvc;

namespace ValeShop.Controllers
{
    public class ShopController : Controller
    {
        // GET
        public IActionResult ShopList()
        {
            return View();
        }
    }
}
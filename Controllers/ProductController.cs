using Microsoft.AspNetCore.Mvc;

namespace ValeShop.Controllers
{
    public class ProductController : Controller
    {
        // GET
        public IActionResult SingleProduct()
        {
            return View();
        }
    }
}
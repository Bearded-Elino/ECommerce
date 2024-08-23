using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ValeShop.PaymentServices;


namespace ECommerce.Controllers
{
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly PaystackService  _paystackService;
        public PaymentController(PaystackService paystackService)
        {
            _paystackService = paystackService;
        }


        [HttpGet("Callback")]

        public async Task<IActionResult> Callback(string reference)
        {
            var response = await _paystackService.VerifyTransactionAsync(reference);
            return View(response);
        }



        public IActionResult Error(string message)
        {
            ViewData["ErrorMessage"] = message;
            return View("Error", new { message });
        }

    }
}
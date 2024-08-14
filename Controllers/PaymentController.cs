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

       [HttpGet("Pay/{amount}/{email}")]
        public async Task<IActionResult> Pay(decimal amount, string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { status = false, message = "Invalid Email Address Passed" });
            }

            try
            {
                var response = await _paystackService.InitializePayment(amount, email);
                var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

                if (responseObject.status == true)
                {
                    var authorizationUrl = responseObject.data.authorization_url;
                    return Redirect(authorizationUrl);
                }
                else
                {
                    return Json(new { status = false, message = responseObject.message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
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
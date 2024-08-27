using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ECommerce.Views.Payment
{
    public class Payment : PageModel
    {
        private readonly ILogger<Payment> _logger;

        public Payment(ILogger<Payment> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ECommerce.Views.Order
{
    public class OrderSuccess : PageModel
    {
        private readonly ILogger<OrderSuccess> _logger;

        public OrderSuccess(ILogger<OrderSuccess> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
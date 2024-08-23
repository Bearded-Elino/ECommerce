using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ECommerce.Views.Search
{
    public class SearchResults : PageModel
    {
        private readonly ILogger<SearchResults> _logger;

        public SearchResults(ILogger<SearchResults> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
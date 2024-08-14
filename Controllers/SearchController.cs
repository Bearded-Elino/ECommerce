using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValeShop.interfaces;
using ValeShop.Models;

namespace ECommerce.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SearchController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("SearchResults/Results")]
        public async Task<IActionResult> Results(string query, Guid? category)
        {
            var categories = await _categoryRepository.ViewCategories();
            ViewBag.Categories = categories;
            ViewBag.SearchQuery = query;

            var results = await _productRepository.SearchProductsAsync(query, category);
            return View("SearchResults", results); // Make sure the view name matches
        }
    }
}

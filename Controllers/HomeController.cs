using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ValeShop.Models;
using ValeShop.ViewModels;
using ValeShop.interfaces; // Adjust the namespace according to your project

namespace ValeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryRepository.ViewCategories(); // Fetch categories
                if (categories == null || !categories.Any())
                {
                    Console.WriteLine("No categories found."); // Logging or other handling if needed
                }

                var categoryModels = _mapper.Map<List<CategoryViewModel>>(categories); // Map to ViewModel
                return View(categoryModels); // Pass the categories to the view
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // Logging the exception
                throw; // Rethrow the exception for further handling
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
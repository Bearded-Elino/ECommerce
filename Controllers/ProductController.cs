using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ILogger<Product> _logger;

        public ProductController(IProductRepository productRepository, IMapper mapper, AppDbContext context, ILogger<Product> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult SingleProduct()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }

            try
            {
                var product = await _productRepository.AddProduct(productViewModel);
                TempData["ProductSuccess"] = "Product successfully added";
                return RedirectToAction("CreateProduct");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not add product");
                ModelState.AddModelError(string.Empty, "Could not add product. Please try again.");
                return View(productViewModel);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var products = await _productRepository.ViewProducts();
                return View(products);
            }
            catch (Exception e)
            {
                return StatusCode(500, new {status = "failed", message = "failed to get product details"});
            }
        }
        
        /*
        [HttpGet]
        [Route("products/productsbycategory/{categoryId}")]
        public async Task<IActionResult> ProductsByCategory(Guid categoryId)
        {
            var products = await _productRepository.ProductsByCategory(categoryId);
            var productViewModels = _mapper.Map<List<ProductViewModel>>(products);
            return View(productViewModels);
        }*/

        [HttpPost]
        public async Task<IActionResult> EditProduct(Guid productId, ProductViewModel productViewModel)
        {
            var product = await _productRepository.EditProduct(productId, productViewModel);
            if (product == null)
            {
                TempData["ProductIdError"] = "ProductId does not exist";
                return RedirectToAction("GetAllProducts", "Product");
            }
    
            var productViewModels = new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
    
            ViewBag.ProductId = productId;
            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult EditProduct()
        {
            return View();
        }
        
        [HttpGet]
        [Route("product/productdetails/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }


        
        [HttpGet]
        [Route("products/productsbycategory/{categoryId}")]
        public async Task<IActionResult> ProductsByCategory(Guid categoryId)
        {
            try
            {
                var products = await _productRepository.ProductsByCategory(categoryId);
                if (products == null || !products.Any())
                {
                    TempData["Message"] = "No products available in this category.";
                    return View("ProductsByCategory", new List<ProductViewModel>());
                }
                var productViewModels = _mapper.Map<List<ProductViewModel>>(products);
                return View(productViewModels);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
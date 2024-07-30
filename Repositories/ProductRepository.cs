using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;
using ValeShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ValeShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly ILogger<Product> _logger;

        public ProductRepository(AppDbContext context, IMapper mapper, IImageRepository imageRepository, ILogger<Product> logger)
        {
            _context = context;
            _mapper = mapper;
            _imageRepository = imageRepository;
            _logger = logger;
        }
        
        public async Task<Product> AddProduct(ProductViewModel productViewModel)
        {
            try
            {
                var product = new Product()
                {
                    Name = productViewModel.Name,
                    Description = productViewModel.Description,
                    CategoryId = productViewModel.CategoryId,
                    Price = productViewModel.Price,
                };

                if (productViewModel.ImageFile != null && productViewModel.ImageFile.Length > 0)
                {
                    var uploadResult = await _imageRepository.UploadImagesAsync(productViewModel.ImageFile);
                    if (!string.IsNullOrEmpty(uploadResult.Url))
                    {
                        product.ImageUrl = uploadResult.Url;
                    }
                }

                var addedProduct = await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return addedProduct.Entity; // Return the added entity
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create product");
                throw new Exception("Failed to create product", ex);
            }
        }


        public async Task<List<ProductViewModel>> ViewProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return _mapper.Map<List<ProductViewModel>>(products);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Product>> ProductsByCategory(Guid categoryId)
        {
            try
            {
                return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception("category not found", e);
            }
        }

        public async Task<Product> EditProduct(Guid productId, ProductViewModel productViewModel)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null)
                {
                    return null;
                }

                product.Name = productViewModel.Name;
                product.Description = productViewModel.Description;
                product.Price = productViewModel.Price;

                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<Product> GetProductById(Guid productId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    return null;
                }
        
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    Console.WriteLine($"Product with ID {productId} not found.");
                }
                return product;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching product: {e.Message}");
                throw;
            }
        }

    }
}
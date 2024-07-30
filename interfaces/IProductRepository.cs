using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.interfaces
{
    public interface IProductRepository
    {
        public Task<Product> AddProduct(ProductViewModel productViewModel);
        public Task<List<ProductViewModel>> ViewProducts();
        public Task<List<Product>>ProductsByCategory(Guid categoryId);
        public Task<Product> EditProduct(Guid productId, ProductViewModel productViewModel);
        public Task<Product> GetProductById(Guid productId);
    }
}
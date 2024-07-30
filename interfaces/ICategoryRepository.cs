using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.interfaces
{
    public interface ICategoryRepository
    {
        public Task<bool> AddCategory(CategoryViewModel categoryViewModel);
        public Task<Category> UpdateCategory(UpdateCategoryViewModel updateCategoryViewModel);
        public Task<List<CategoryViewModel>> ViewCategories();
        public Task<Category> DeleteCategory(Guid parentId);
    }
}
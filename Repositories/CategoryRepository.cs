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

namespace ValeShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<bool> AddCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var existingCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryName.Equals(categoryViewModel.CategoryName, StringComparison.OrdinalIgnoreCase));

                if (existingCategory != null)
                {
                    return false; 
                }

                var category = _mapper.Map<Category>(categoryViewModel);

                if (categoryViewModel.Id.HasValue)
                {
                    var parentCategory = await _context.Categories
                        .FirstOrDefaultAsync(c => c.Id == categoryViewModel.Id.Value);

                    if (parentCategory == null)
                    {
                        return false;
                    }

                    category.Parent = parentCategory;
                }

                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public async Task<Category> UpdateCategory( UpdateCategoryViewModel updateCategoryViewModel)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == updateCategoryViewModel.Id);
            if (category == null)
            {
                return null;
            }

            category.CategoryName = updateCategoryViewModel.CategoryName;
            category.CategoryDescription = updateCategoryViewModel.CategoryDescription;
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<CategoryViewModel>> ViewCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                return _mapper.Map<List<CategoryViewModel>>(categories);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get categories", e);
            }
        }

        public async Task<Category> DeleteCategory(Guid parentId)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == parentId);
                if (category == null)
                {
                    return null;
                }

                _context.Remove(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
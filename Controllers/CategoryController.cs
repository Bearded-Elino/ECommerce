using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValeShop.interfaces;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET
        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryRepository.ViewCategories();
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");

                return View(categoryViewModel);
            }

            bool isAdded = await _categoryRepository.AddCategory(categoryViewModel);

            if (isAdded)
            {
                TempData["Message"] = "Category added successfully.";
                return RedirectToAction("ViewAllCategories", "Category");
            }
            else
            {
                TempData["Message"] = "Category already exists. Please choose a different name.";

                var categories = await _categoryRepository.ViewCategories();
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");

                return View(categoryViewModel);
            }
        }


        
        
        [HttpGet]
        public IActionResult UpdateCategory()
        {
            TempData["UpdateSuccess"] = "Category updated successfully";
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryViewModel updateCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("updateCategory", "Category");
            }

            try
            {
                var category =await _categoryRepository.UpdateCategory( updateCategoryViewModel);
                if (category == null)
                {
                    return View();
                }

                TempData["UpdateSuccess"] = "Category updated successfully";
                return RedirectToAction("index", "Home");

            }
            catch (Exception e)
            {
                TempData["UpdateFailed"] = "Category update failed";
                throw new Exception("Cannot update Category");
            }

        }

        [HttpGet]
        public async Task<IActionResult> ViewAllCategories()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var categories = await _categoryRepository.ViewCategories();
                var categoryModels = _mapper.Map<List<CategoryViewModel>>(categories);
                return View(categoryModels);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCategory(Guid parentId)
        {
            if (!ModelState.IsValid)
            {
                TempData["DeleteFailed"] = "Invalid operation";
                return RedirectToAction("DeleteCategory");
            }

            try
            {
                var category =await _categoryRepository.DeleteCategory(parentId);
                if (category == null)
                {
                    TempData["DeleteFailed"] = "category not found";
                    return RedirectToAction("DeleteCategory");
                }

                TempData["DeleteSuccess"] = "category deleted successfully";
                return RedirectToAction("DeleteCategory");
            }
            catch (Exception e)
            {
                TempData["DeleteFailed"] = "category failed to delete";
                Console.WriteLine(e);
                return RedirectToAction("DeleteCategory");
            }
        }

        public IActionResult DeleteCategory()
        {
            return View();
        }






    }
    
}
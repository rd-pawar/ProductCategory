using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategory.Data;
using ProductCategory.Models;
using ProductCategory.Models.Domain;
using System;

namespace ProductCategory.Controllers
{
    public class AddCategoryController : Controller
    {
        private readonly ModelDbContext modelDbContext;

        public AddCategoryController(ModelDbContext modelDbContext)        {            this.modelDbContext = modelDbContext;        }
        [HttpGet]        public async Task<IActionResult> Index()
        {            var categories = await modelDbContext.Category.ToListAsync();            return View(categories);        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        public async Task<IActionResult> AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            var category = new Category()
            {
                CategoryName = addCategoryViewModel.CategoryName
            };
            await modelDbContext.Category.AddAsync(category);            await modelDbContext.SaveChangesAsync();            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var category = await modelDbContext.Category.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (category != null) 
            {
                var viewModel = new UpdateCategoryViewModel()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateCategoryViewModel updateCategoryViewModel) 
        {
            var category = await modelDbContext.Category.FindAsync(updateCategoryViewModel.CategoryId);
            if (category != null)
            {
                category.CategoryName = updateCategoryViewModel.CategoryName;

                await modelDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(UpdateCategoryViewModel delete) 
        {
            //var category = await modelDbContext.Category.FirstOrDefaultAsync(x => x.CategoryId==id);
            var category = await modelDbContext.Category.FindAsync(delete.CategoryId);
            if (category != null) 
            {
                modelDbContext.Category.Remove(category);
                await modelDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}

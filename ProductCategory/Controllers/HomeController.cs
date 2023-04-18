using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategory.Data;
using ProductCategory.Models;
using ProductCategory.Models.Domain;
using System.Collections;
using System.Diagnostics;
//using PagedList.Mvc;
//using PagedList;

namespace ProductCategory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelDbContext modelDbContext;

        public HomeController(ModelDbContext modelDbContext)
        {
            this.modelDbContext = modelDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            var pr = modelDbContext.Product;
            var ct = modelDbContext.Category;
            var product = (from p in pr
                          join c in ct on p.CategoryId equals c.CategoryId
                select new
                {
                    ProductId = p.Id,
                    ProductName = p.ProductName,
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                }).ToList();
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct() 
        {
            List<string> st= new List<string>();
                var category = await modelDbContext.Category.ToListAsync();
                AddProductViewModel apvdm = new AddProductViewModel();
                foreach (var item in category) 
                {
                    st.Add(item.CategoryName);
                }
            apvdm.LCategory = st;
            return View(apvdm);
        } 

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel addProductViewModel)
        {
            var ct = modelDbContext.Category;
            var re = ct.FirstOrDefault(x => x.CategoryName == addProductViewModel.Categories);
            Product product = new Product()
            {
                ProductName = addProductViewModel.ProductName,
                CategoryId = re.CategoryId
            };
            await modelDbContext.AddAsync(product);
            await modelDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var product = await modelDbContext.Product.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                var viewModel = new ProductDetails()
                {
                    ProductName = product.ProductName,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(ProductDetails productDetails)
        {
            var product = await modelDbContext.Product.FindAsync(productDetails.Id);
            if (product != null)
            {
                product.ProductName = productDetails.ProductName;

                await modelDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductDetails delete)
        {
            //var category = await modelDbContext.Category.FirstOrDefaultAsync(x => x.CategoryId==id);
            var product = await modelDbContext.Product.FindAsync(delete.Id);
            if (product != null)
            {
                modelDbContext.Product.Remove(product);
                await modelDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
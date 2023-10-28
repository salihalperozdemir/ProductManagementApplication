using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.Entities.Models;

namespace ProductManagement.Web.Controllers
{
    //Manager and Seller can use this pages 
    [Authorize(Roles = "Manager,Seller")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //List view for categories
        public IActionResult Index()
        {
            var categoryList = new List<Category>();
            try
            {
                categoryList = _categoryService.GetCategories();
                return View(categoryList);
            }
            catch (Exception ex)
            {
                return View(categoryList);
            }
            
        }
        //Detail informations about category
        [HttpGet]
        public async Task<ActionResult> Get(int categoryId)
        {
            try
            {
                var category = _categoryService.GetCategory(categoryId);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //New category create method
        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                var categoryResponse = _categoryService.AddCategory(category);
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        //Update category method
        [HttpPost]
        public async Task<ActionResult> Update(Category category)
        {
            try
            {
                var categoryResponse = _categoryService.UpdateCategory(category);
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //Delete category method
        [HttpGet]
        public async Task<ActionResult> Delete(int categoryId)
        {
            try
            {
                var categoryResponse = _categoryService.DeleteCategory(categoryId);
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}

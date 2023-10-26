using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.Dto.Dto;
using ProductManagement.Entities.Models;

namespace ProductManagement.Web.Controllers
{
    [Authorize(Roles = "Manager,Seller")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categoryList = _categoryService.GetCategories();
            return View(categoryList);
        }
        public async Task<IActionResult> New()
        {
            return View();
        }
        public async Task<IActionResult> Edit()
        {
            return View();
        }
        public async Task<IActionResult> Detail()
        {
            return View();
        }


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

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                var categoryList = _categoryService.GetCategories();
                return Ok(categoryList);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

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

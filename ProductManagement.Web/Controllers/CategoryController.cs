using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.Dto.Dto;

namespace ProductManagement.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
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
        public async Task<ActionResult> GetCategory(int categoryId)
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
        public async Task<ActionResult> CreateCategory(CategoryDto category)
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
        public async Task<ActionResult> UpdateCategory(CategoryDto category)
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

        [HttpPost]
        public async Task<ActionResult> DeleteCategory(int categoryId)
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

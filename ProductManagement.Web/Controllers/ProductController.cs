using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Business.Services;
using ProductManagement.Core.Model;
using ProductManagement.Entities.Models;
using ProductManagement.Web.Models;

namespace ProductManagement.Web.Controllers
{
    [Authorize(Roles = "Seller,Manager")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        public ProductController(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var productListViewModel = new ProductListViewModel();
            try
            {
                var userModel = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<LoginResponse>(userModel);
         
                productListViewModel.Products = _productService.GetProductsByCompanyId(user.CompanyId);
                productListViewModel.Categories = _categoryService.GetCategories().Select(x => new KeyValue { Key = x.CategoryId, Value = x.Name }).ToList();
             
            }
            catch (Exception ex)
            {

            }
            return View(productListViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> GetProduct(int productId)
        {
            try
            {
                var product = _productService.GetProduct(productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        private async Task<ActionResult> GetProducts()
        {
            try
            {
                var userModel = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<LoginResponse>(userModel);
                var productListViewModel = new List<ProductListViewModel>();
                var productList = _productService.GetProductsByCompanyId(user.CompanyId);
                var categories = _categoryService.GetCategories().Select(x => new KeyValue { Key = x.CategoryId, Value = x.Name }).ToList();
                return Ok(productListViewModel);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                var userModel = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<LoginResponse>(userModel);
                product.CompanyId = user.CompanyId;
                var productResponse = _productService.AddProduct(product);
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Product product)
        {
            try
            {
                var productResponse = _productService.UpdateProduct(product);
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int productId)
        {
            try
            {
                var productResponse = _productService.DeleteProduct(productId);
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Business.Services;
using ProductManagement.Core.Model;
using ProductManagement.Dto.Dto;
using ProductManagement.Web.Models;
using System.Globalization;

namespace ProductManagement.Web.Controllers
{
    //Only Manager and Seller can see this screen
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
        
        //Product list page 
        public IActionResult Index()
        {
            var productListViewModel = new ProductListViewModel();
            try
            {
                var userModel = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<LoginResponse>(userModel);
                productListViewModel.Products = _productService.GetProductsByCompanyId(user.CompanyId);

                //Get categories for selectbox
                productListViewModel.Categories = _categoryService.GetCategories().Select(x => new KeyValue { Key = x.CategoryId, Value = x.Name }).ToList();
                return View(productListViewModel);
            }
            catch (Exception ex)
            {
                return View(productListViewModel);
            }
        }
        //Get details from product
        [HttpGet]
        public async Task<ActionResult> Get(int productId)
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
        //Create new product method
        [HttpPost]
        public async Task<ActionResult> Create(ProductDto product)
        {
            try
            {
                //Formatting price for decimal
                decimal parsedPrice;
                if (decimal.TryParse(product.PriceString, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out parsedPrice))
                {
                    product.Price = parsedPrice;
                }
                else
                {
                    parsedPrice = 0;
                }
                //Get company information from user 
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
        //Update new product method
        [HttpPost]
        public async Task<ActionResult> Update(ProductDto product)
        {
            try
            {
                //Formatting price for decimal
                decimal parsedPrice;
                if (decimal.TryParse(product.PriceString, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out parsedPrice))
                {
                    product.Price = parsedPrice;
                }
                else
                {
                    parsedPrice = 0;
                }
                //Get company information from user 
                var userModel = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<LoginResponse>(userModel);
                product.CompanyId = user.CompanyId;

                var productResponse = _productService.UpdateProduct(product);
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //Delete product method
        [HttpGet]
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

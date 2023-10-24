using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.Dto.Dto;

namespace ProductManagement.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
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
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                var productList = _productService.GetProducts();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDto product)
        {
            try
            {
                var productResponse = _productService.AddProduct(product);
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateProduct(ProductDto product)
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
        public async Task<ActionResult> DeleteCategory(int productId)
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

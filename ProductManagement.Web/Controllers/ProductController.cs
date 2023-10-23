using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Web.Controllers
{
    public class ProductController : Controller
    {
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
    }
}

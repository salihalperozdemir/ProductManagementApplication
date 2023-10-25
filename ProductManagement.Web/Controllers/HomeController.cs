using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Web.Models;
using System.Diagnostics;

namespace ProductManagement.Web.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }   
        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult Forbidden()
        {
            return View();
        }
        public IActionResult UnAuthorized()
        {
            return View();
        }
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    return View("Forbidden");
                }
                else if (statusCode == 403)
                {
                    return View("Forbidden");
                }
                else if (statusCode == 401)
                {
                    return View("Forbidden");
                }
            }
            // Handle other error cases or return a generic error view.
            return View("Error");
        }
        //[HttpGet]
        //public IActionResult Error()
        //{
        //    var statusCode = HttpContext.Request.Query["statusCode"];
        //    if (statusCode == "404")
        //    {
        //        return View("Forbidden");
        //    }
        //    else if (statusCode == "403")
        //    {
        //        return View("Forbidden");
        //    }
        //    else if (statusCode == "401")
        //    {
        //        return View("Forbidden");
        //    }
        //    // Handle other error cases or return a generic error view
        //    return View("Error");
        //}
    }
}
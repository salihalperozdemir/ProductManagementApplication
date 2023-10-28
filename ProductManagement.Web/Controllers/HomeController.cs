using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Business.Services;
using ProductManagement.Entities.Models;
using ProductManagement.Web.Models;
using System.Diagnostics;

namespace ProductManagement.Web.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OrderService _orderService;
        private readonly UserService _userServices;
        private readonly ProductService _productService;
        private readonly CompanyService _companyService;
        public HomeController(ILogger<HomeController> logger, OrderService orderService, UserService userService, ProductService productService, CompanyService companyService)
        {
            _logger = logger;
            _orderService = orderService;
            _userServices = userService;
            _productService = productService;
            _companyService = companyService;
        }

        //Dashboard page
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel();
            //Logged user model get from session
            var userModel = HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<LoginResponse>(userModel);

            //Get orders from user
            if (user.Roles.Contains("Customer"))
            {
                homeViewModel.Orders = _orderService.GetOrdersByCustomerId(user.UserId);
            }
            else if (user.Roles.Contains("Manager"))
            {
                homeViewModel.Orders = _orderService.GetOrders();
            }
            else
            {
                homeViewModel.Orders = _orderService.GetOrdersByCompanyId(user.CompanyId);
            }
            //For card informations get data count
            homeViewModel.CustomerCount = _userServices.GetCustomers().Result.Count;
            homeViewModel.ProductCount = _productService.GetProducts().Count;
            homeViewModel.CompanyCount = _companyService.GetCompanies().Count;

            return View(homeViewModel);
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
        //Error redirections
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    return View("NotFound");
                }
                else if (statusCode == 403)
                {
                    return View("UnAuthorized");
                }
                else if (statusCode == 401)
                {
                    return View("Forbidden");
                }
            }
            // Handle other error cases or return a generic error view.
            return View("Error");
        }
    }
}
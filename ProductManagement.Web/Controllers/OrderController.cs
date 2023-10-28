using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Business.Services;
using ProductManagement.Dto.Dto;
using ProductManagement.Entities.Models;

namespace ProductManagement.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly CategoryService _categoryService;
        private readonly CompanyService _companyService;
        private readonly ProductService _productService;
        private readonly UserService _userServices;
        public OrderController(OrderService orderService, CategoryService categoryService, CompanyService companyService, ProductService productService, UserService userServices)
        {
            _orderService = orderService;
            _categoryService = categoryService;
            _companyService = companyService;
            _productService = productService;
            _userServices = userServices;
        }
        
        //Get list of orders
        public IActionResult Index()
        {
            var orderListViewModel = new List<Order>();
            try
            {
                //Get orders by role if manager sees everything customer only see which are belongs to, and seller sees only it's company have
                var userModel = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<LoginResponse>(userModel);
                if (user.Roles.Contains("Customer"))
                {
                    orderListViewModel = _orderService.GetOrdersByCustomerId(user.UserId);
                }
                else if (user.Roles.Contains("Manager"))
                {
                    orderListViewModel = _orderService.GetOrders();
                }
                else
                {
                    orderListViewModel = _orderService.GetOrdersByCompanyId(user.CompanyId);
                }
            }
            catch (Exception ex)
            {

            }
            return View(orderListViewModel);
        }
        //For new order page design with need data for selectboxes and for creator
        public async Task<IActionResult> New()
        {
            var orderViewModel = new OrderViewModelDto();
            try
            {
                //selectbox data
                orderViewModel.Categories = _categoryService.GetCategories();
                orderViewModel.Brand = _companyService.GetCompanies();
                orderViewModel.Products = _productService.GetProducts();
                //user data
                var userModel = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<LoginResponse>(userModel);
                orderViewModel.User = new AppUser { FirstName = user.FirstName, Id = user.UserId, LastName = user.LastName, Email = user.Email };
                return View(orderViewModel);
            }
            catch (Exception ex)
            {
                return View(orderViewModel);
            }
           
        }
        //Preview for order
        public async Task<IActionResult> Detail()
        {
            var orderViewModel = new OrderViewModelDto();
            try
            {
                //Get order detail
                var orderId = Convert.ToInt32(Request.Query["orderId"]);
                orderViewModel = _orderService.GetOrder(orderId).OrderDto;

                //These data for product list view
                orderViewModel.Categories = _categoryService.GetCategories();
                orderViewModel.Brand = _companyService.GetCompanies();
                orderViewModel.Products = _productService.GetProducts();
                orderViewModel.User = new AppUser { FirstName = orderViewModel.User.FirstName, Id = orderViewModel.User.Id, LastName = orderViewModel.User.LastName, Email = orderViewModel.User.Email };

                return View(orderViewModel);
            }
            catch (Exception ex)
            {
                return View(orderViewModel);
            }
         
        }
        //Create order method
        [HttpPost]
        public async Task<ActionResult> Create(OrderViewModelDto order)
        {
            try
            {
                //Get customer data from logged in
                var userModel = HttpContext.Session.GetString("User");
                var user = JsonConvert.DeserializeObject<LoginResponse>(userModel);
                order.CustomerId = user.UserId;
                var orderResponse = _orderService.AddOrder(order);
                //after adding order redirect to list
                orderResponse.ReturnUrl = "/Order/Index";
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //Delete order
        [HttpGet]
        public async Task<ActionResult> Delete(int orderId)
        {
            try
            {
                var orderResponse = _orderService.DeleteOrder(orderId);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

    }
}

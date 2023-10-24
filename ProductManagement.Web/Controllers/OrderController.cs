using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.Dto.Dto;

namespace ProductManagement.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
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
        public async Task<ActionResult> GetOrder(int orderId)
        {
            try
            {
                var order = _orderService.GetOrder(orderId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var orderList = _orderService.GetOrders();
                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDto order)
        {
            try
            {
                var orderResponse = _orderService.AddOrder(order);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateOrder(OrderDto order)
        {
            try
            {
                var orderResponse = _orderService.UpdateOrder(order);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOrder(int orderId)
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

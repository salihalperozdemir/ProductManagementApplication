using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.DAL.Interfaces;
using ProductManagement.Dto.Dto;
using ProductManagement.Dto.Interfaces;
using ProductManagement.Entities.Models;

namespace ProductManagement.Business.Services
{
    public class OrderService
    {
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public OrderService(IOrderProductRepository orderProductRepository, IOrderRepository orderRepository,
            IProductRepository productRepository, IUserRepository userRepository)
        {
            _orderProductRepository = orderProductRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }


        //Getting products which belongs to order for order detail
        public List<OrderProduct> GetOrderProducts(int orderId)
        {
            var listProducts = new List<OrderProduct>();
            try
            {
                listProducts = GetOrderProductsByOrderId(orderId);
                return listProducts;
            }
            catch (Exception ex)
            {
                return listProducts;
            }
        }
        //Get customer's orders
        public List<Order> GetOrdersByCustomerId(int userId)
        {
            //These joins are for getting specific properties from products and customer
            var orderList = new List<Order>();
            try
            {
                 orderList = _orderRepository.GetAll(x => x.CustomerId == userId)
                                              .Join(
                                                  _orderProductRepository.GetAll(),
                                                  order => order.OrderId,
                                                  orderProduct => orderProduct.OrderId,
                                                  (order, orderProduct) => orderProduct)
                                              .Join(
                                                  _productRepository.GetAll(),
                                                  orderProduct => orderProduct.ProductId,
                                                  product => product.ProductId,
                                                  (orderProduct, product) => orderProduct.Order)
                                              .Distinct().ToList();
                return orderList.Join(
                                _userRepository.GetAll(),
                                order => order.CustomerId,
                                user => user.Id,
                                (order, user) => new { Order = order, User = user }).Select(x => new Order { User = x.User, OrderNumber = x.Order.OrderNumber, Price = x.Order.Price, OrderId = x.Order.OrderId })
                            .ToList();
            }
            catch (Exception ex)
            {
                return orderList;
            }

        }
        //Get seller's orders
        public List<Order> GetOrdersByCompanyId(int companyId)
        {
            //These joins are for getting specific properties from products and customer

            var orderList = new List<Order>();
            try
            {
                orderList = _orderRepository.GetAll()
                                            .Join(
                                                _orderProductRepository.GetAll(),
                                                order => order.OrderId,
                                                orderProduct => orderProduct.OrderId,
                                                (order, orderProduct) => orderProduct)
                                            .Join(
                                                _productRepository.GetAll(x => x.CompanyId == companyId),
                                                orderProduct => orderProduct.ProductId,
                                                product => product.ProductId,
                                                (orderProduct, product) => orderProduct.Order)
                                            .Distinct().ToList();

                return orderList.Join(
                            _userRepository.GetAll(),
                            order => order.CustomerId,
                            user => user.Id,
                            (order, user) => new { Order = order, User = user }).Select(x => new Order { User = x.User, OrderNumber = x.Order.OrderNumber, Price = x.Order.Price, OrderId = x.Order.OrderId })
                        .ToList();
            }
            catch (Exception ex)
            {
                return orderList;
            }
        }
        //Get order detail
        public OrderResponse GetOrder(int orderId)
        {
            var response = new OrderResponse();
            try
            {
                var order = _orderRepository.GetById(orderId);

                if (order != null)
                {
                    var user = _userRepository.GetById(order.CustomerId);
                    response.OrderDto = new OrderViewModelDto
                    {
                        OrderId = order.OrderId,
                        OrderNumber = order.OrderNumber,
                        Price = order.Price,
                        User = user,
                        CustomerId = order.CustomerId,
                        OrderProducts = GetOrderProductsByOrderId(orderId),
                        Phone = order.Phone,
                        Address = order.Address
                    };
                    response.IsOk = true;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Order not found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
        }
        //Create new order
        public OrderResponse AddOrder(OrderViewModelDto order)
        {
            var response = new OrderResponse { IsOk = true };
            try
            {
                var lastOrderId = _orderRepository.GetAll().Count();
                lastOrderId++;
                decimal orderPrice = 0;
                var createOrder = new Order
                {
                    OrderNumber = "Order" + lastOrderId.ToString().PadLeft(3, '0'),
                    Address = order.Address,
                    Phone = order.Phone,
                    CustomerId = order.CustomerId
                };
                var orderResponse = _orderRepository.Add(createOrder);

                if (orderResponse != null)
                {
                    var orderId = orderResponse.OrderId;

                    if (order.OrderProducts.Count > 0)
                    {
                        foreach (var item in order.OrderProducts)
                        {
                            //product assigns to order
                            var product = _productRepository.GetById(item.ProductId);
                            var orderProduct = new OrderProduct
                            {
                                OrderId = orderId,
                                ProductId = item.ProductId,
                                Quantity = item.Quantity,
                            };
                            var productResponse = _orderProductRepository.Add(orderProduct);
                            if (productResponse != null)
                            {
                                orderPrice += product.Price * item.Quantity;
                                continue;
                            }
                            else
                            {
                                response.IsOk = false;
                                response.Message = "Adding product to order is getting error.";
                                return response;
                            }

                        }
                    }
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Order creating is unsuccesfull";
                }
                createOrder.Price = orderPrice;
                var updateResponse = _orderRepository.Update(createOrder);
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
        }

        //Get list of orders for manager role
        public List<Order> GetOrders()
        {
            var orderList = _orderRepository.GetAll().ToList();
            return orderList.Join(
                                _userRepository.GetAll(),
                                order => order.CustomerId,
                                user => user.Id,
                                (order, user) => new { Order = order, User = user }).Select(x => new Order { User = x.User, OrderNumber = x.Order.OrderNumber, Price = x.Order.Price, OrderId = x.Order.OrderId })
                            .ToList();
        }
        //Delete order from system
        public OrderResponse DeleteOrder(int orderId)
        {
            var response = new OrderResponse { IsOk = false };
            try
            {
                var order = _orderRepository.GetById(orderId);
                if (order == null)
                {
                    response.IsOk = false;
                    response.Message = "Order is not found";
                    return response;
                }

                var products = _orderProductRepository.GetAll(x => x.OrderId == orderId).ToList();
                if (products != null && products.Count > 0)
                {
                    foreach (var item in products)
                    {
                        _orderProductRepository.Delete(item.OrderProductId);
                    }
                }
                _orderRepository.Delete(order);
                response.IsOk = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
        }

        #region Helper methods
        private List<OrderProduct> GetOrderProductsByOrderId(int orderId)
        {
            var listProducts = new List<OrderProduct>();
            try
            {
                listProducts = _orderProductRepository.GetAll(orderProduct => orderProduct.OrderId == orderId)
                                               .Join(_productRepository.GetAll(),
                                                   orderProduct => orderProduct.ProductId,
                                                   product => product.ProductId,
                                                   (orderProduct, product) => new OrderProduct
                                                   {
                                                       OrderId = orderProduct.OrderId,
                                                       Product = product,
                                                       ProductId = orderProduct.ProductId,
                                                       Quantity = orderProduct.Quantity,
                                                       OrderProductId = orderProduct.OrderProductId
                                                   })
                                               .ToList();
                return listProducts;
            }
            catch (Exception ex)
            {
                return listProducts;
            }
        }

        #endregion
    }
}

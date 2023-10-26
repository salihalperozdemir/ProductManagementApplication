﻿using ProductManagement.Business.Models.ResponseModels;
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



        public List<Product> GetOrderProducts(int orderId)
        {
            var listProducts = new List<Product>();
            listProducts = GetOrderProductsByOrderId(orderId);
            return listProducts;
        }
        public List<Order> GetOrdersByCompanyId(int companyId, int userId = 0)
        {
            if(userId == 0)
            {
                var orderList = _orderRepository
                   .GetAll()
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
                   .ToList();
                return orderList
                            .Join(
                                _userRepository.GetAll(),
                                order => order.CustomerId,
                                user => user.Id,
                                (order, user) => new { Order = order, User = user }).Select(x => new Order { User = x.User, OrderNumber = x.Order.OrderNumber, Price = x.Order.Price, OrderId = x.Order.OrderId })
                            .ToList();
            }
            else
            {
                var orderList = _orderRepository
                   .GetAll(x => x.CustomerId == userId)
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
                   .ToList();
                return orderList
                            .Join(
                                _userRepository.GetAll(),
                                order => order.CustomerId,
                                user => user.Id,
                                (order, user) => new { Order = order, User = user }).Select(x => new Order { User = x.User, OrderNumber = x.Order.OrderNumber, Price = x.Order.Price, OrderId = x.Order.OrderId })
                            .ToList();
            }
           
           


        }


        private List<Product> GetOrderProductsByOrderId(int orderId)
        {
            var listProducts = new List<Product>();
            listProducts = _orderProductRepository.GetAll(orderProduct => orderProduct.OrderId == orderId)
                                                .Join(_productRepository.GetAll(),
                                                    orderProduct => orderProduct.ProductId,
                                                    product => product.ProductId,
                                                    (orderProduct, product) => product)
                                                .ToList();

            return listProducts;
        }
        public OrderResponse GetOrder(int orderId)
        {
            var response = new OrderResponse();
            try
            {
                var order = _orderRepository.GetById(orderId);

                if (order != null)
                {
                    response.OrderDto = new OrderDto
                    {
                        OrderId = order.OrderId,
                        OrderNumber = order.OrderNumber,
                        Price = order.Price,
                        User = order.User,
                        CustomerId = order.User.Id,
                        Products = GetOrderProductsByOrderId(orderId)
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
        public OrderResponse AddOrder(OrderDto order)
        {
            var response = new OrderResponse { IsOk = true };
            try
            {
                var createOrder = new Order
                {
                    OrderNumber = order.OrderNumber,
                    Price = order.Price,
                    CustomerId = order.CustomerId
                };
                var orderResponse = _orderRepository.Add(createOrder);

                if (orderResponse != null)
                {
                    var orderId = orderResponse.OrderId;

                    if (order.Products.Count > 0)
                    {
                        foreach (var item in order.Products)
                        {
                            var orderProduct = new OrderProduct
                            {
                                OrderId = order.OrderId,
                                ProductId = item.ProductId,
                            };
                            var productResponse = _orderProductRepository.Add(orderProduct);
                            if (productResponse != null)
                            {
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
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
        }
        public OrderResponse UpdateOrder(OrderDto order)
        {
            var response = new OrderResponse { IsOk = true };
            if (order.OrderId == 0)
            {
                response.IsOk = false;
                response.Message = "Order is not found";
            }
            try
            {
                var orderExist = _orderRepository.GetById(order.OrderId);
                if (orderExist == null)
                {
                    response.IsOk = false;
                    response.Message = "Order is not found";
                }
                var orderResponse = _orderRepository.Update(order);

                if (orderResponse != null)
                {
                    var orderProducts = _orderProductRepository.GetAll(x => x.OrderId == order.OrderId);

                    foreach (var item in orderProducts)
                    {
                        _orderProductRepository.Delete(item.OrderProductId);
                    }
                    if (order.Products.Count > 0)
                    {
                        foreach (var item in order.Products)
                        {
                            var orderProduct = new OrderProduct
                            {
                                OrderId = order.OrderId,
                                ProductId = item.ProductId,
                            };
                            var productResponse = _orderProductRepository.Add(orderProduct);
                            if (productResponse != null)
                            {
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
                    response.Message = "Order updating is unsuccesfull";
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
        public List<Order> GetOrders()
        {
            var orderList = _orderRepository.GetAll().ToList();

            return orderList;
        }
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

                _orderRepository.Delete(order);

                var products = _orderProductRepository.GetAll(x => x.OrderId == orderId).ToList();
                if (products != null && products.Count > 0)
                {
                    foreach (var item in products)
                    {
                        _orderProductRepository.Delete(item.OrderProductId);
                    }
                }

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
    }
}

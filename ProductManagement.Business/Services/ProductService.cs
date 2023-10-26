﻿using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Core.Model;
using ProductManagement.DAL.Repositories;
using ProductManagement.Dto.Dto;
using ProductManagement.Dto.Interfaces;
using ProductManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Business.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {

            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public List<KeyValue> GetProductList(int companyId = 0)
        {
            var listProducts = new List<KeyValue>();
            if (companyId == 0)
            {
                listProducts = _productRepository.GetAll().Select(x => new KeyValue { Key = x.ProductId, Value = x.Code + " " + x.Name }).ToList();
            }
            else
            {
                listProducts = _productRepository.GetAll(x => x.CompanyId == companyId).Select(x => new KeyValue { Key = x.ProductId, Value = x.Code + " " + x.Name }).ToList();
            }
            return listProducts;
        }
        public ProductResponse GetProduct(int productId)
        {
            var response = new ProductResponse();
            try
            {
                var product = _productRepository.GetById(productId);

                if (product != null)
                {
                    response.ProductId = product.ProductId;
                    response.Name = product.Name;
                    response.Code = product.Code;
                    response.Price = product.Price;
                    response.CategoryId = product.CategoryId;
                    response.IsOk = true;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Product not found";
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
        public ProductResponse AddProduct(Product product)
        {
            var response = new ProductResponse { IsOk = true };
            try
            {
                var productResponse = _productRepository.Add(product);

                if (productResponse != null)
                {
                    response.IsOk = true;
                    response.ProductId = productResponse.ProductId;
                    response.Name = productResponse.Name;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Product creating is unsuccesfull";
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
        public ProductResponse UpdateProduct(Product product)
        {
            var response = new ProductResponse { IsOk = true };
            if (product.ProductId == 0)
            {
                response.IsOk = false;
                response.Message = "Product is not found";
            }
            try
            {
                var productExist = _productRepository.GetById(product.CompanyId);
                if (productExist == null)
                {
                    response.IsOk = false;
                    response.Message = "Product is not found";
                }
                var productResponse = _productRepository.Update(product);

                if (productResponse != null)
                {
                    response.IsOk = true;
                    response.ProductId = productResponse.ProductId;
                    response.Name = productResponse.Name;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Product updating is unsuccesfull";
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
        public List<Product> GetProducts()
        {
            var productList = _productRepository.GetAll().ToList();

            return productList;
        }
        public List<Product> GetProductsByCompanyId(int companyId)
        {
            if (companyId == 1)
            {
                return _productRepository
                        .GetAll()
                        .Join(
                            _categoryRepository.GetAll(),
                            product => product.CategoryId,
                            category => category.CategoryId,
                            (product, category) => new Product
                            {
                                ProductId = product.ProductId,
                                Name = product.Name,
                                Code = product.Code,
                                Price = product.Price,
                                Category = category,
                            })
                        .ToList();
            }
            else
            {
                return _productRepository.GetAll(x => x.CompanyId == companyId)
                        .Join(
                            _categoryRepository.GetAll(),
                            product => product.CategoryId,
                            category => category.CategoryId,
                            (product, category) => new Product
                            {
                                ProductId = product.ProductId,
                                Name = product.Name,
                                Code = product.Code,
                                Price = product.Price,
                                Category = category,
                            })
                        .ToList();
            }

        }
        public ProductResponse DeleteProduct(int productId)
        {
            var response = new ProductResponse { IsOk = false };
            try
            {
                var product = _productRepository.GetById(productId);
                if (product == null)
                {
                    response.IsOk = false;
                    response.Message = "Product is not found";
                    return response;
                }

                _productRepository.Delete(product);

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

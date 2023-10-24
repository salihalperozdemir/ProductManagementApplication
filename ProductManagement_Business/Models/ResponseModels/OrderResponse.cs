﻿using ProductManagement.Core.Response;
using ProductManagement.Dto.Dto;

namespace ProductManagement.Business.Models.ResponseModels
{
    public class OrderResponse : BaseResponse
    {
        public OrderDto OrderDto { get; set; }

    }
}

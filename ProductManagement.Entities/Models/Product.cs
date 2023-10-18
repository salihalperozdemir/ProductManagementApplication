﻿using System.ComponentModel.DataAnnotations;

namespace ProductManagement_DAL.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
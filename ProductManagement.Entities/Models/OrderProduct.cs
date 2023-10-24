﻿using System.ComponentModel.DataAnnotations;

namespace ProductManagement_DAL.Models
{
    public class OrderProduct
    {
        [Key]
        public int OrderProductId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

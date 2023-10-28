using ProductManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Dto.Dto
{
    public class OrderViewModelDto : OrderDto
    {
        public OrderViewModelDto()
        {
            Categories = new List<Category>();
            Brand = new List<Company>();
            OrderProducts = new List<OrderProduct>();
        }
        public List<Category> Categories { get; set; }
        public List<Company> Brand { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}

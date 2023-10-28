using ProductManagement.Entities.Models;

namespace ProductManagement.Dto.Dto
{
    public class OrderDto : Order
    {
        public OrderDto()
        {
            Products = new List<Product>();
        }
        public List<Product> Products { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
    }
}

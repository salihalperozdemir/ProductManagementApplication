using ProductManagement_DAL.Models;

namespace ProductManagement.Dto.Dto
{
    public class OrderDto : Order
    {
        public List<Product> Products { get; set; }
    }
}

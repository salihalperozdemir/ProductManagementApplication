using ProductManagement.Entities.Models;

namespace ProductManagement.Dto.Dto
{
    public class ProductDto : Product
    {
        public string PriceString { get; set; }
        public int Quantity { get; set; }
    }
}

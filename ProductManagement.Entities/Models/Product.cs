using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Entities.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}

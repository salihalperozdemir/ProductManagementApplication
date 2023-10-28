using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Entities.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal Price { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CustomerId { get; set; }
        public AppUser User { get; set; }
    }
}

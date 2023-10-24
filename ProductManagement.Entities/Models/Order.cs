using System.ComponentModel.DataAnnotations;

namespace ProductManagement_DAL.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal Price { get; set; }
        public int CustomerId { get; set; }
        public AppUser User { get; set; }
    }
}

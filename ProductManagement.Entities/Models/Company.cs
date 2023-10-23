using System.ComponentModel.DataAnnotations;

namespace ProductManagement_DAL.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool Valid { get; set; }
        public List<Product> Products { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}

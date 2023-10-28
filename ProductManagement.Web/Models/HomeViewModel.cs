using ProductManagement.Entities.Models;

namespace ProductManagement.Web.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Orders = new List<Order>();
        }
        public List<Order> Orders { get; set; }
        public int CompanyCount { get; set; }
        public int CustomerCount { get; set; }
        public int ProductCount { get; set; }
    }
}

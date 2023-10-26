using ProductManagement.Core.Model;
using ProductManagement.Entities.Models;

namespace ProductManagement.Web.Models
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            Products = new List<Product>();
            Categories = new List<KeyValue>();
        }
        public List<Product> Products { get; set; }
        public List<KeyValue> Categories { get; set; }
    }
}

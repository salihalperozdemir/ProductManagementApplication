using System.ComponentModel.DataAnnotations;

namespace ProductManagement_DAL.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool Valid { get; set; }
        public List<Product> Products { get; set; }
    }
}

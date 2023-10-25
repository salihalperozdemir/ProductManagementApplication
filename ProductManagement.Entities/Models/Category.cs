using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Entities.Models
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

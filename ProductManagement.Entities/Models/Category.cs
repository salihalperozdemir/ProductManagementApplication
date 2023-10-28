using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Entities.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}

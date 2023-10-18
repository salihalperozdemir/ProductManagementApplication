using System.ComponentModel.DataAnnotations;

namespace ProductManagement_DAL.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Valid { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}

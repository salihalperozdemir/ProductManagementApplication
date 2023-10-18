using System.ComponentModel.DataAnnotations;

namespace ProductManagement_DAL.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public bool Valid { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

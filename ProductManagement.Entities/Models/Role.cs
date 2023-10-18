using System.ComponentModel.DataAnnotations;

namespace ProductManagement_DAL.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public int Name { get; set; }
        public bool Valid { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}

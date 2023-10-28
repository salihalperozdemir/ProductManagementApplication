using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Entities.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
    }
}

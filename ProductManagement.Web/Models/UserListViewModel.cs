using ProductManagement.Core.Model;
using ProductManagement.Entities.Models;

namespace ProductManagement.Web.Models
{
    public class UserListViewModel
    {
        public List<AppUser> Users { get; set; }
        public List<KeyValue> Companies { get; set; }
    }
}

using ProductManagement.Core.Model;
using ProductManagement.DAL.Dto;

namespace ProductManagement.Web.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Companies = new List<KeyValue>();
        }
        public List<KeyValue> Companies { get; set; }
        public UserViewModelDto User { get; set; }
    }
}

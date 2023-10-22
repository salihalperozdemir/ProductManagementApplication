using ProductManagement.Core.Response;
using ProductManagement_DAL.Models;
using System.ComponentModel;

namespace ProductManagement.Business.Models.ResponseModels
{
	public class LoginResponseModel : BaseResponse
	{
        public LoginResponseModel()
        {
        }
        public int UserId { get; set; }
		public int CompanyId { get; set; }
		public string FirstName { get; set; }
		public string Email { get; set; }
		public string LastName { get; set; }
		public string Token { get; set; }
        public IList<string> Roles { get; set; }
    }
	
}

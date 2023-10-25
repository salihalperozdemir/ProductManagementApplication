using ProductManagement.Core.Response;
using ProductManagement.Entities.Models;
using System.ComponentModel;

namespace ProductManagement.Business.Models.ResponseModels
{
	public class LoginResponse : BaseResponse
	{
        public LoginResponse()
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

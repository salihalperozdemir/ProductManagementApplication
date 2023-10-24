using ProductManagement.Core.Response;

namespace ProductManagement.Business.Models.ResponseModels
{
	public class SignupResponse : BaseResponse
	{
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

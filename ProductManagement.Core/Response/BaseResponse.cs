namespace ProductManagement.Core.Response
{
	public class BaseResponse
	{
        public bool IsOk { get; set; }
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
    }
}

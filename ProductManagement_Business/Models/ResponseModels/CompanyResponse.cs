using ProductManagement.Core.Response;

namespace ProductManagement.Business.Models.ResponseModels
{
    public class CompanyResponse : BaseResponse
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
    }
}

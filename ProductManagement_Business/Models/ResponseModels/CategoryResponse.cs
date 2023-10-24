using ProductManagement.Core.Response;

namespace ProductManagement.Business.Models.ResponseModels
{
    public class CategoryResponse : BaseResponse
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
       
    }
}

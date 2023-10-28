using ProductManagement.Core.Response;

namespace ProductManagement.Business.Models.ResponseModels
{
    public class ProductResponse : BaseResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
       
    }
}

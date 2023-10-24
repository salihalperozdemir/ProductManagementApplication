using ProductManagement_DAL.Models;

namespace ProductManagement.Dto.Dto
{
    public class CompanyDto : Company
    {
        public CompanyDto Map(Company company)
        {
            CompanyDto result = new CompanyDto();
            result.CompanyId = company.CompanyId;
            result.CompanyName = company.CompanyName;

            return result;
        }
    }

}

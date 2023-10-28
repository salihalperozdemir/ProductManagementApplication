using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Core.Model;
using ProductManagement.Dto.Interfaces;
using ProductManagement.Entities.Models;

namespace ProductManagement.Business.Services
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {

            _companyRepository = companyRepository;
        }
        //List companies for selectbox data
        public List<KeyValue> GetCompanyList()
        {
            var listCompanies = new List<KeyValue>();
            try
            {
                listCompanies = _companyRepository.GetAll().Select(x => new KeyValue { Key = x.CompanyId, Value = x.CompanyName }).ToList();

                return listCompanies;
            }
            catch (Exception ex)
            {
                return listCompanies;
            }
            
        }
        //Get company detail
        public CompanyResponse GetCompany(int companyId)
        {
            var response = new CompanyResponse();
            try
            {
                var company = _companyRepository.GetById(companyId);

                if (company != null)
                {
                    response.CompanyId = company.CompanyId;
                    response.Name = company.CompanyName;
                    response.IsOk = true;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Company not found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }      
        }
        //Create new company
        public CompanyResponse AddCompany(Company company)
        {
            var response = new CompanyResponse { IsOk = true };
            try
            {
                var companyResponse = _companyRepository.Add(company);

                if (companyResponse != null)
                {
                    response.IsOk = true;
                    response.CompanyId = companyResponse.CompanyId;
                    response.Name = companyResponse.CompanyName;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Company creating is unsuccesfull";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
        }
        //Update company
        public CompanyResponse UpdateCompany(Company company)
        {
            var response = new CompanyResponse { IsOk = true };
            if(company.CompanyId == 0)
            {
                response.IsOk = false;
                response.Message = "Company is not found";
            }
            try
            {
                var companyExist = _companyRepository.GetById(company.CompanyId);
                if(companyExist == null)
                {
                    response.IsOk = false;
                    response.Message = "Company is not found";
                }
                var companyResponse = _companyRepository.Update(company);

                if (companyResponse != null)
                {
                    response.IsOk = true;
                    response.CompanyId = companyResponse.CompanyId;
                    response.Name = companyResponse.CompanyName;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Company updating is unsuccesfull";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }

        }
        
        //Get all companies for listing
        public List<Company> GetCompanies()
        {
            var companyList = new List<Company>();
            try
            {
                companyList = _companyRepository.GetAll().ToList();

                return companyList;
            }
            catch (Exception ex)
            {
                return companyList;
            }
          
        }
        //Delete company from system
        public CompanyResponse DeleteCompany(int companyId)
        {
            var response = new CompanyResponse { IsOk = false };
            try
            {
                var company = _companyRepository.GetById(companyId);
                if(company == null)
                {
                    response.IsOk = false;
                    response.Message = "Company is not found";
                    return response;
                }

                _companyRepository.Delete(company);

                response.IsOk = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}

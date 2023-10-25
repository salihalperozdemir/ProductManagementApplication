using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Core.Model;
using ProductManagement.Core.Response;
using ProductManagement.DAL.Repositories;
using ProductManagement.Dto.Dto;
using ProductManagement.Dto.Interfaces;
using ProductManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Business.Services
{
    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {

            _companyRepository = companyRepository;
        }
        public List<KeyValue> GetCompanyList()
        {
            var listCompanies = new List<KeyValue>();

            listCompanies = _companyRepository.GetAll().Select(x => new KeyValue { Key = x.CompanyId, Value = x.CompanyName }).ToList();

            return listCompanies;
        }
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
        public List<Company> GetCompanies()
        {
            var companyList = _companyRepository.GetAll().ToList();

            return companyList;
        }
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

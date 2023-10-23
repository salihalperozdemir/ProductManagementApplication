using ProductManagement.Core.Model;
using ProductManagement.Dto.Interfaces;
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
    }
}

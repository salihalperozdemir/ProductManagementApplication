using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.Entities.Models;

namespace ProductManagement.Web.Api.Controllers
{
    //Only customer role can access to these methods
    [ApiController]
    [Authorize(Roles="Customer")]
    public class CompanyController : Controller
    {
        private readonly CompanyService _companyService;
        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        #region CRUD operations for Company
        [HttpGet]
        [Route("Get/{companyId}")]
        public async Task<ActionResult> GetCompany(int companyId)
        {
            try
            {
                var company = _companyService.GetCompany(companyId);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> GetCompanies()
        {
            try
            {
                var companyList = _companyService.GetCompanies();
                return Ok(companyList);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateCompany(Company company)
        {
            try
            {
                var companyResponse = _companyService.AddCompany(company);
                return Ok(companyResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> UpdateCompany(Company company)
        {
            try
            {
                var companyResponse = _companyService.UpdateCompany(company);
                return Ok(companyResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("Delete/{companyId}")]
        public async Task<ActionResult> DeleteCompany(int companyId)
        {
            try
            {
                var companyResponse = _companyService.DeleteCompany(companyId);
                return Ok(companyResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        #endregion


    }
}

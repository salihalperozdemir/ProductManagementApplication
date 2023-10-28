using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.Entities.Models;

namespace ProductManagement.Web.Controllers
{
    //Only manager can access
    [Authorize(Roles = "Manager")]
    public class CompanyController: Controller
    {
        private readonly CompanyService _companyService;
        public CompanyController(CompanyService companyService)
        {
            _companyService  = companyService;
        }

        //List view for companies
        public async Task<IActionResult> Index()
        {
            var companies = new List<Company>();
            try
            {
                companies = _companyService.GetCompanies();
                return View(companies);
            }
            catch (Exception ex)
            {

                companies = _companyService.GetCompanies();
                return View(companies);
            }
        }
        //Detail informations about company
        [HttpGet]
        public async Task<ActionResult> Get(int companyId)
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

        //New company create method
        [HttpPost]
        public async Task<ActionResult> Create(Company company)
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
        //Update company method
        [HttpPost]
        public async Task<ActionResult> Update(Company company)
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
        //Delete company method
        [HttpGet]
        public async Task<ActionResult> Delete(int companyId)
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

    }
}

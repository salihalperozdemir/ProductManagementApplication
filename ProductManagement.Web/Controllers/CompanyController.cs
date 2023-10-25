using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.Dto.Dto;
using ProductManagement.Entities.Models;

namespace ProductManagement.Web.Controllers
{
    public class CompanyController: Controller
    {
        private readonly CompanyService _companyService;
        public CompanyController(CompanyService companyService)
        {
            _companyService  = companyService;
        }
        public async Task<IActionResult> Index()
        {
            var companies = _companyService.GetCompanies();
            return View(companies);
        }
        public async Task<IActionResult> New()
        {
            return View();
        }
        public async Task<IActionResult> Edit()
        {
            return View();
        }
        public async Task<IActionResult> Detail()
        {
            return View();
        }

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

        [HttpGet]
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.DAL.Dto;
using ProductManagement.Web.Models;
using ProductManagement_Business.Enum;
using ProductManagement_DAL.Models;
using System.Data.Entity;

namespace ProductManagement.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserServices _userService;
        private readonly CompanyService _companyService;
        
        public UserController(UserManager<AppUser> userManager, UserServices userServices, CompanyService companyService)
        {
            _userManager = userManager;
            _userService = userServices;
            _companyService = companyService;
        }


        public async Task<IActionResult> Index()
        {
            var listUsers = _userManager.Users.ToList();

            return View(listUsers);
        }

        public async Task<IActionResult> Detail(int userId)
        {
            var userModel = _userService.GetUserById(userId).Result;
            var companyList = _companyService.GetCompanyList();
            var userViewModel = new UserViewModel
            {
                User = userModel,
                Companies = companyList
            };

            return View(userViewModel);
        }

        public async Task<IActionResult> New()
        {
            var companyList = _companyService.GetCompanyList();
            
            return View(companyList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModelDto user)
        {
            var userResponse = await _userService.CreateUser(user);
            if(userResponse.IsOk) {
                userResponse.ReturnUrl = "/User/Index";
            }
            return Ok(userResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModelDto user)
        {
            var userResponse = await _userService.UpdateUser(user);
            if (userResponse.IsOk)
            {
                userResponse.ReturnUrl = "/User/Index";
            }
            return Ok(userResponse);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int userId)
        {
            var userResponse = await _userService.DeleteUser(userId);
            if (userResponse.IsOk)
            {
                return RedirectToAction("Index", "User");
            }
            return Ok(userResponse);
        }
        [HttpGet]
        public async Task<List<AppUser>> ListOfAllUsers()
        {

            var listUsers = _userManager.Users.ToList();

            return listUsers;
        }
    }
}

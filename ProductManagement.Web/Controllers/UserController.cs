using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement.DAL.Dto;
using ProductManagement.Entities.Models;
using ProductManagement.Web.Models;

namespace ProductManagement.Web.Controllers
{
    [Authorize(Roles ="Manager")]
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
            var companies = _companyService.GetCompanyList();
            var userListViews = new UserListViewModel
            {
                Users = listUsers,
                Companies = companies
            };
            return View(userListViews);
        }

        //public async Task<IActionResult> Detail(int userId)
        //{
        //    var userModel = _userService.GetUserById(userId).Result;
        //    var companyList = _companyService.GetCompanyList();
        //    var userViewModel = new UserViewModel
        //    {
        //        User = userModel,
        //        Companies = companyList
        //    };

        //    return View(userViewModel);
        //}

        //public async Task<IActionResult> New()
        //{
        //    var companyList = _companyService.GetCompanyList();
            
        //    return View(companyList);
        //}
        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            var userResponse = new UserViewModelDto();
            userResponse = _userService.GetUserById(userId).Result;
            return Ok(userResponse);
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
               
            }
            return Ok(userResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int userId)
        {
            var userResponse = await _userService.DeleteUser(userId);
            if (userResponse.IsOk)
            {
                
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Business.Services;
using ProductManagement.Core.Response;
using ProductManagement.DAL.Dto;
using ProductManagement.Entities.Models;
using ProductManagement.Web.Models;

namespace ProductManagement.Web.Controllers
{
    [Authorize(Roles ="Manager")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserService _userService;
        private readonly CompanyService _companyService;
        
        public UserController(UserManager<AppUser> userManager, UserService userServices, CompanyService companyService)
        {
            _userManager = userManager;
            _userService = userServices;
            _companyService = companyService;
        }

        //List of users page 
        public async Task<IActionResult> Index()
        {
            var listUsers = _userManager.Users.ToList();
            //Get all companies for selectbox
            var companies = _companyService.GetCompanyList();
            var userListViews = new UserListViewModel
            {
                Users = listUsers,
                Companies = companies
            };
            return View(userListViews);
        }
        //Get user detail
        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            var userResponse = new UserViewModelDto();
            try
            {
                userResponse = _userService.GetUserById(userId).Result;
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Ok(userResponse);
                
            }
           
        }
        //Create user method
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModelDto user)
        {
            var userResponse = new SignupResponse();
            try
            {
                userResponse = await _userService.CreateUser(user);
                if (userResponse.IsOk)
                {
                    //redirection after creation
                    userResponse.ReturnUrl = "/User/Index";
                }
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                userResponse.IsOk = false;
                userResponse.Message = ex.Message;
                return Ok(userResponse);
            }
        }
        //Update user method
        [HttpPost]
        public async Task<IActionResult> Update(UserViewModelDto user)
        {
            var userResponse = new BaseResponse();
            try
            {
                userResponse = await _userService.UpdateUser(user);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                userResponse.IsOk = false;
                userResponse.Message = ex.Message;
                return Ok(userResponse);
            }
        }
        //Delete user method
        [HttpGet]
        public async Task<IActionResult> Delete(int userId)
        {
            var userResponse = new BaseResponse();
            try
            {
                userResponse = await _userService.DeleteUser(userId);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                userResponse.IsOk = false;
                userResponse.Message = ex.Message;
                return Ok(userResponse);
            }
         
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Services;
using ProductManagement_DAL.Models;
using System.Data.Entity;

namespace ProductManagement.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserServices _userService;
        public UserController(UserManager<AppUser> userManager, UserServices userServices)
        {
            _userManager = userManager;
            _userService = userServices;
        }


        public async Task<IActionResult> Index()
        {
            var listUsers = _userManager.Users.ToList();

            return View(listUsers);
        }

        public async Task<IActionResult> Detail(int userId)
        {
            var userModel = _userService.GetUserById(userId).Result;

            return View(userModel);
        }


        [HttpGet]
        public async Task<List<AppUser>> ListOfAllUsers()
        {

            var listUsers = _userManager.Users.ToList();

            return listUsers;
        }
    }
}

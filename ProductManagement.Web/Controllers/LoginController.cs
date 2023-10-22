using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Business.Models;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Business.Services;
using ProductManagement.Web.Helpers;
using ProductManagement.Web.Models;
using ProductManagement_DAL.Models;
using System.Text.Json.Serialization;

namespace ProductManagement.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private UserServices _userServices;
        private AuthHelper _authHelper = new AuthHelper();
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
        public LoginController(UserServices userServices, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _userServices = userServices;
			_signInManager = signInManager;
			_userManager = userManager;

		}
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Signup(SignupViewModel model)
        {
            try
            {
				var userResponse = await _userServices.SignUp(model);
				return Ok(userResponse);
			}
            catch (Exception ex)
            {
				return Ok(new SignupResponseModel { IsOk = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            if (result.Succeeded)
            {
                var user = await _userServices.GetUserByEmail(model.Email);
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                var tokenResponse = _authHelper.CreateToken(user);

                return Ok(new LoginResponseModel { IsOk = true, Message = "Login is successfull", ReturnUrl = "/Home/Index", Token= tokenResponse.Token});
            }
            else
            {
                return Ok(new LoginResponseModel { IsOk = false, Message = "Login is unsuccessfull" });
            }

		}


    }
}

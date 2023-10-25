using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Business.Models;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Business.Services;
using ProductManagement.DAL.Dto;
using ProductManagement.Entities.Models;
using ProductManagement.Web.Helpers;
using ProductManagement.Web.Models;

namespace ProductManagement.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private UserServices _userServices;
        private AuthHelper _authHelper = new AuthHelper();
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
        private IConfiguration _configuration;
        public LoginController(UserServices userServices, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userServices = userServices;
			_signInManager = signInManager;
			_userManager = userManager;
            _configuration = configuration;

        }
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Signup(UserViewModelDto model)
        {
            try
            {
				var userResponse = await _userServices.CreateUser(model);
				return Ok(userResponse);
			}
            catch (Exception ex)
            {
				return Ok(new SignupResponse { IsOk = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginViewModelDto model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
                if (result.Succeeded)
                {
                    var user = await _userServices.GetUserByEmail(model.Email);
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                    var tokenResponse = _authHelper.CreateToken(user, _configuration);
                    HttpContext.Session.SetString("Token", tokenResponse.Token);

                    return Ok(new LoginResponse { IsOk = true, Message = "Login is successfull", ReturnUrl = "/Home/Index", Token = tokenResponse.Token });
                }
                else
                {
                    return Ok(new LoginResponse { IsOk = false, Message = "Login is unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new LoginResponse { IsOk = false, Message = "Login is unsuccessfull" });
            }
			

		}


    }
}

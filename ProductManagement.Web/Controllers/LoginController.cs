using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Business.Services;
using ProductManagement.DAL.Dto;
using ProductManagement.Entities.Models;
using ProductManagement.Web.Helpers;

namespace ProductManagement.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private UserService _userServices;
        private AuthHelper _authHelper = new AuthHelper();
		private readonly SignInManager<AppUser> _signInManager;
        private IConfiguration _configuration;
        public LoginController(UserService userServices, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userServices = userServices;
			_signInManager = signInManager;
            _configuration = configuration;

        }
        //Login page
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

        //Signup method for saving user
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

        //Login to system method
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginViewModelDto model)
        {
            try
            {
                //Credential check for login
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
                if (result.Succeeded)
                {
                    //Storing user data after login
                    var user = await _userServices.GetUserByEmail(model.Email);
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                    //Generating jwt token for using API
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

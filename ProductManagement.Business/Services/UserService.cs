using Microsoft.AspNetCore.Identity;
using ProductManagement.Business.Enum;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Core.Response;
using ProductManagement.DAL.Dto;
using ProductManagement.DAL.Interfaces;
using ProductManagement.Entities.Models;

namespace ProductManagement.Business.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        //Create user method and assing default customer and company All

        public async Task<SignupResponse> CreateUser(UserViewModelDto model)
        {
            var response = new SignupResponse();
            try
            {

                if (model != null)
                {
                    var checkEmail = await _userManager.FindByEmailAsync(model.Email);

                    if (checkEmail != null)
                    {
                        response.IsOk = false;
                        response.Message = "Email already exist.";
                        return response;
                    }
                    else
                    {
                        var user = new AppUser
                        {
                            Email = model.Email,
                            UserName = model.Email,
                            FirstName = model.FirstName,
                            CompanyId = model.CompanyId != 0 ? model.CompanyId : 1,
                            RoleId = model.RoleId != 0 ? model.RoleId : 1,
                            LastName = model.LastName,
                            Password = model.Password
                        };
                        var userResponse = await _userManager.CreateAsync(user, user.Password);

                        if (userResponse.Succeeded)
                        {
                            var userRoleAssign = new IdentityResult();
                            switch (user.RoleId)
                            {
                                case 1:
                                    userRoleAssign = await _userManager.AddToRoleAsync(user, "Customer");
                                    break;
                                case 2:
                                    userRoleAssign = await _userManager.AddToRoleAsync(user, "Manager");
                                    break;
                                case 3:
                                    userRoleAssign = await _userManager.AddToRoleAsync(user, "Seller");
                                    break;
                                default:

                                    break;
                            }


                            if (userRoleAssign.Succeeded)
                            {
                                response.IsOk = true;
                                response.Email = model.Email;
                                response.Password = model.Password;

                            }

                        }
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsOk = false;
                return response;
            }
        }

        //Get user informations by email information
        public async Task<LoginResponse> GetUserByEmail(string email)
        {
            var response = new LoginResponse();
            try
            {
                var user = _userRepository.GetAll().Where(x => x.Email == email).FirstOrDefault();


                if (user != null)
                {
                    response.UserId = user.Id;
                    response.Email = email;
                    response.FirstName = user.FirstName;
                    response.LastName = user.LastName;
                    response.CompanyId = user.CompanyId;
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Count > 0)
                    {
                        response.Roles = roles;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
           

        }
        //Get user informations by user id
        public async Task<UserViewModelDto> GetUserById(int userId)
        {
            var userViewModel = new UserViewModelDto();
            var user = _userRepository.GetById(userId);
            if (user != null)
            {

                userViewModel.RoleId = user.RoleId;
                userViewModel.FirstName = user.FirstName;
                userViewModel.LastName = user.LastName;
                userViewModel.Email = user.Email;
                userViewModel.CompanyId = user.CompanyId;
                userViewModel.Password = user.Password;
                userViewModel.UserId = userId;
                return userViewModel;
            }
            else
            {
                return null;
            }
        }

        //Update user method for controlling role and company
        public async Task<BaseResponse> UpdateUser(UserViewModelDto userViewModel)
        {
            var response = new BaseResponse();
            var oldUser = _userRepository.GetById(userViewModel.UserId);

            var oldUserRole = System.Enum.GetName(typeof(RoleTypes), oldUser.RoleId);
            var user = new AppUser
            {
                Id = userViewModel.UserId,
            };
            if (oldUser.RoleId != userViewModel.RoleId)
            {
                var newRole = System.Enum.GetName(typeof(RoleTypes), userViewModel.RoleId);


                var removeRoleFromUser = await _userManager.RemoveFromRoleAsync(oldUser, oldUserRole);
                if (removeRoleFromUser.Succeeded)
                {
                    var assignNewRole = await _userManager.AddToRoleAsync(oldUser, newRole);
                    if (!assignNewRole.Succeeded)
                    {
                        response.IsOk = false;
                        response.Message = "Assigning user role is getting error";
                    }
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Removing user role is getting error";
                }
            }
            oldUser.FirstName = userViewModel.FirstName;
            oldUser.LastName = userViewModel.LastName;
            oldUser.Email = userViewModel.Email;
            oldUser.Password = userViewModel.Password;
            oldUser.CompanyId = userViewModel.CompanyId;
            oldUser.RoleId = userViewModel.RoleId;

            var updateResponse = await _userManager.UpdateAsync(oldUser);
            if (updateResponse.Succeeded)
            {
                response.IsOk = true;

            }
            else
            {
                response.IsOk = false;
                response.Message = "Updating user is getting error";
            }
            return response;
        }

        //Delete user method
        public async Task<BaseResponse> DeleteUser(int userId)
        {
            var response = new BaseResponse { IsOk = false };
            var user = _userRepository.GetById(userId);
            if (user != null)
            {
                var deleteResponse = await _userManager.DeleteAsync(user);
                if (deleteResponse.Succeeded)
                {
                    response.IsOk = true;

                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Deleting user is getting error";
                }
            }
            else
            {
                response.Message = "User is not found";
            }
            return response;
        }

        //Get all customer for listing
        public async Task<List<AppUser>> GetCustomers()
        {
            var userList = _userManager.Users.Where(x => x.RoleId == (int)RoleTypes.Customer).ToList();

            return userList;
        }


    }
}

using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductManagement.Business.Models;
using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.DAL.Dto;
using ProductManagement.DAL.Interfaces;
using ProductManagement.DAL.Repositories;
using ProductManagement_Business.Enum;
using ProductManagement_DAL.Models;
using System.Data;
using System.Data.Entity.Core.Mapping;
using System.Globalization;
using System.Net.Http;

namespace ProductManagement.Business.Services
{
	public class UserServices
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserRoleRepository _userRoleRepository;
		private readonly UserManager<AppUser> _userManager;
		public UserServices(IUserRepository userRepository, IUserRoleRepository userRoleRepository, UserManager<AppUser> userManager)
        {
			_userRepository = userRepository;
			_userRoleRepository = userRoleRepository;
			_userManager = userManager;
		}

		public async Task<SignupResponseModel> SignUp(UserViewModelDto model)
		{
			var response = new SignupResponseModel();
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
							LastName = model.LastName,
							Password = model.Password,
							Valid = true
						};
						var userResponse = await _userManager.CreateAsync(user, user.Password);

						if (userResponse.Succeeded)
						{
							var userRoleAssign = await _userManager.AddToRoleAsync(user, "Customer");

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

		public async Task<LoginResponseModel> GetUserByEmail(string email)
		{
			var user = _userRepository.GetAll().Where(x => x.Email == email).FirstOrDefault();
			var response = new LoginResponseModel();

            if (user != null)
			{
                response.Email = email;
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;

                var roles = await _userManager.GetRolesAsync(user);
				if(roles.Count > 0)
				{
					response.Roles = roles;
				}
			}
			return response;
			
		}

		public async Task<UserViewModelDto> GetUserById(int userId)
		{
			var userViewModel = new UserViewModelDto();
			var user = _userRepository.GetById(userId);
			if (user != null)
			{
				userViewModel.RoleId = await GetUserRole(user);
				userViewModel.FirstName = user.FirstName;
				userViewModel.LastName = user.LastName;
				userViewModel.Email = user.Email;
				userViewModel.Password = user.Password;
				userViewModel.UserId = userId;
				return userViewModel;
			}
			else
			{
				return null;
			}
		}

		private async Task<int> GetUserRole(AppUser user)
		{
			var checkCustomerRole = await _userManager.IsInRoleAsync(user, "Customer");
			var checkSellerRole = await _userManager.IsInRoleAsync(user, "Seller");
			var checkAdminRole = await _userManager.IsInRoleAsync(user, "Administrator");

			if (checkCustomerRole) return (int)RoleTypes.Customer;
			if (checkSellerRole) return (int)RoleTypes.Seller;
			if (checkAdminRole) return (int)RoleTypes.Administrator;

			return 0;
		}


	}
}

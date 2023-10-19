using ProductManagement.DAL.Interfaces;
using ProductManagement.DAL.Repositories;
using ProductManagement_Business.Enum;
using ProductManagement_DAL.Data;
using ProductManagement_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement_Business.Helpers
{
    public class UserHelper
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private UserRepository _userRepository;
        public UserHelper()
        {
            _userRepository = new UserRepository();
        }
        public async Task<User> CreateUser(User signupViewModel)
        {
            try
            {
                if(signupViewModel != null)
                {
                    _userRepository.Add(signupViewModel);
                }
               
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}

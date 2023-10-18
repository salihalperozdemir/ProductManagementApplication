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

        public async Task<User> CreateUser(User signupViewModel)
        {
            try
            {
                if(signupViewModel != null)
                {
                    var obj = _dbContext.Add<User>(signupViewModel);
                    await _dbContext.SaveChangesAsync();

                    var userRoleModel = new UserRole { CompanyId = 0, User = signupViewModel, RoleId = (int)RoleTypes.Customer };
                    var userRole = _dbContext.Add<UserRole>(userRoleModel);

                    await _dbContext.SaveChangesAsync();
                    return obj.Entity;
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

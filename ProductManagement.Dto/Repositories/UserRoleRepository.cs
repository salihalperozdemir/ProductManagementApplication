using ProductManagement.DAL.Interfaces;

using ProductManagement_DAL.Data;
using ProductManagement.Entities.Models;
using System.Linq.Expressions;

namespace ProductManagement.DAL.Repositories
{
    public class UserRoleRepository : GenericRepository<AppRole>, IUserRoleRepository
	{
		public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
		{

		}
		public int GetUserRoleByUserId(int userId)
		{

			//var roleId = GetAll().Where(x => x.UserId == userId).FirstOrDefault().RoleId;

			return 0;
		}

		//public void Update(UserRole entity)
  //      {
  //          if(entity != null)
  //          {
  //              var userRole = _dbContext.UserRoles.Where(x => x.UserId == entity.UserId).FirstOrDefault();
  //              if(userRole != null)
  //              {
		//			userRole = entity;
  //                  _dbContext.UserRoles.Update(userRole);
  //              }
  //              _dbContext.SaveChanges();
  //          }
  //      }
    }
}

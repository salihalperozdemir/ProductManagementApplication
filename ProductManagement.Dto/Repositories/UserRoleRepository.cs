using ProductManagement.DAL.Interfaces;

using ProductManagement_DAL.Data;
using ProductManagement_DAL.Models;
using System.Linq.Expressions;

namespace ProductManagement.DAL.Repositories
{
    public class UserRoleRepository : GenericRepository<AppRole>, IUserRoleRepository
	{
		public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
		{

		}
        
  //      public UserRole Add(UserRole entity)
  //      {
  //          if(entity != null)
  //          {
  //              var userRoleResponse = _dbContext.Add<UserRole>(entity);
		//		_dbContext.SaveChanges();
		//		entity = userRoleResponse.Entity;
                           
  //          }
  //          return entity;
  //      }

  //      public void Delete(UserRole entity)
  //      {
  //          var userRole = _dbContext.UserRoles.Where(x => x.UserRoleId == entity.UserId).FirstOrDefault();
  //          if(userRole != null)
  //          {
		//		userRole.Valid = false;
  //          }
  //          _dbContext.SaveChanges();
  //      }

  //      public void Delete(int id)
  //      {
  //          var userRole = _dbContext.UserRoles.Where(x => x.UserRoleId == id).FirstOrDefault();
  //          if (userRole != null)
  //          {
		//		userRole.Valid = false;
  //          }
  //          _dbContext.SaveChanges();
  //      }

  //      public UserRole Get(Expression<Func<UserRole, bool>> predicate)
  //      {
  //          var userRole = _dbContext.UserRoles.Where(predicate).FirstOrDefault();
  //          return userRole;
  //      }

  //      public IQueryable<UserRole> GetAll()
  //      {
  //          var allUserRoleList = _dbContext.UserRoles;
  //          return allUserRoleList;
  //      }

  //      public IQueryable<UserRole> GetAll(Expression<Func<UserRole, bool>> predicate)
  //      {
  //          var userRoleList = _dbContext.UserRoles.Where(predicate);
  //          return userRoleList;
  //      }

  //      public UserRole GetById(int id)
  //      {
  //          var userRole = _dbContext.UserRoles.Where(x => x.UserRoleId == id).FirstOrDefault();
  //          return userRole;
  //      }

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

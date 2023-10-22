using ProductManagement.DAL.Interfaces;
using ProductManagement_DAL.Models;
//using ProductManagement.DAL.Data;
using System.Linq.Expressions;
using ProductManagement_DAL.Data;

namespace ProductManagement.DAL.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
        //    public User Add(User entity)
    //    {
    //        if(entity != null)
    //        {
    //            var userResponse = _dbContext.Add<User>(entity);
				//_dbContext.SaveChanges();
				//entity = userResponse.Entity;
                           
    //        }
    //        return entity;
    //    }

    //    public void Delete(User entity)
    //    {
    //        var user = _dbContext.Users.Where(x => x.UserId == entity.UserId).FirstOrDefault();
    //        if(user != null)
    //        {
    //            user.Valid = false;
    //        }
    //        _dbContext.SaveChanges();
    //    }

    //    public void Delete(int id)
    //    {
    //        var user = _dbContext.Users.Where(x => x.UserId ==id).FirstOrDefault();
    //        if (user != null)
    //        {
    //            user.Valid = false;
    //        }
    //        _dbContext.SaveChanges();
    //    }

    //    public User Get(Expression<Func<User, bool>> predicate)
    //    {
    //        var user = _dbContext.Users.Where(predicate).FirstOrDefault();
    //        return user;
    //    }

    //    public IQueryable<User> GetAll()
    //    {
    //        var userList = _dbContext.Users;
    //        return userList;
    //    }

    //    public IQueryable<User> GetAll(Expression<Func<User, bool>> predicate)
    //    {
    //        var userList = _dbContext.Users.Where(predicate);
    //        return userList;
    //    }

    //    public User GetById(int id)
    //    {
    //        var user = _dbContext.Users.Where(x => x.UserId == id).FirstOrDefault();
    //        return user;
    //    }

    //    public void Update(User entity)
    //    {
    //        if(entity != null)
    //        {
    //            var user = _dbContext.Users.Where(x => x.UserId == entity.UserId).FirstOrDefault();
    //            if(user != null)
    //            {
    //                user = entity;
    //                _dbContext.Users.Update(user);
    //            }
    //            _dbContext.SaveChanges();
    //        }
    //    }
        public bool CheckExistingEmail(string email)
        {
            return GetAll().Where(x => x.Email == email && x.Valid).Any();
        }
    }
}

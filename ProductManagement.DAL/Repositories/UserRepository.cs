using ProductManagement.DAL.Interfaces;
using ProductManagement_DAL.Data;
using ProductManagement_DAL.Models;
using System.Linq.Expressions;

namespace ProductManagement.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public void Add(User entity)
        {
            if(entity != null)
            {
                _dbContext.Users.Add(entity);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(User entity)
        {
            var user = _dbContext.Users.Where(x => x.UserId == entity.UserId).FirstOrDefault();
            if(user != null)
            {
                user.Valid = false;
            }
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.Where(x => x.UserId ==id).FirstOrDefault();
            if (user != null)
            {
                user.Valid = false;
            }
            _dbContext.SaveChanges();
        }

        public User Get(Expression<Func<User, bool>> predicate)
        {
            var user = _dbContext.Users.Where(predicate).FirstOrDefault();
            return user;
        }

        public IQueryable<User> GetAll()
        {
            var userList = _dbContext.Users;
            return userList;
        }

        public IQueryable<User> GetAll(Expression<Func<User, bool>> predicate)
        {
            var userList = _dbContext.Users.Where(predicate);
            return userList;
        }

        public User GetById(int id)
        {
            var user = _dbContext.Users.Where(x => x.UserId == id).FirstOrDefault();
            return user;
        }

        public void Update(User entity)
        {
            if(entity != null)
            {
                var user = _dbContext.Users.Where(x => x.UserId == entity.UserId).FirstOrDefault();
                if(user != null)
                {
                    user = entity;
                    _dbContext.Users.Update(user);
                }
                _dbContext.SaveChanges();
            }
        }
    }
}

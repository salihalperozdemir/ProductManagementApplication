using ProductManagement.DAL.Interfaces;
using ProductManagement.Entities.Models;
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
        public bool CheckExistingEmail(string email)
        {
            return GetAll().Where(x => x.Email == email).Any();
        }
    }
}

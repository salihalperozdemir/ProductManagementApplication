using ProductManagement.Entities.Models;

namespace ProductManagement.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        bool CheckExistingEmail(string email);
    }
}

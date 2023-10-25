using ProductManagement.Entities.Models;

namespace ProductManagement.DAL.Interfaces
{
    public interface IUserRoleRepository : IGenericRepository<AppRole>
	{
		int GetUserRoleByUserId(int userId);
	}
}

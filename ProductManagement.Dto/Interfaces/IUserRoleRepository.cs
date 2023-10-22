using ProductManagement_DAL.Models;

namespace ProductManagement.DAL.Interfaces
{
    public interface IUserRoleRepository : IGenericRepository<AppRole>
	{
		int GetUserRoleByUserId(int userId);
	}
}

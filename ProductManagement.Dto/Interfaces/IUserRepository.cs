using ProductManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        bool CheckExistingEmail(string email);
    }
}

using ProductManagement.DAL.Repositories;
using ProductManagement.Dto.Interfaces;
using ProductManagement_DAL.Data;
using ProductManagement.Entities.Models;

namespace ProductManagement.Dto.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}

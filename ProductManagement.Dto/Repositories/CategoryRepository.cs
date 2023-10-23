using ProductManagement.DAL.Interfaces;
using ProductManagement_DAL.Models;
//using ProductManagement.DAL.Data;
using System.Linq.Expressions;
using ProductManagement_DAL.Data;
using ProductManagement.Dto.Interfaces;

namespace ProductManagement.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
      
    }
}

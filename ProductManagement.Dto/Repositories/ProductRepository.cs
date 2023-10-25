using ProductManagement.DAL.Interfaces;
using ProductManagement.Entities.Models;
//using ProductManagement.DAL.Data;
using System.Linq.Expressions;
using ProductManagement_DAL.Data;
using ProductManagement.Dto.Interfaces;

namespace ProductManagement.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
      
    }
}

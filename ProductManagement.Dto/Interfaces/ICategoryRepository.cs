using ProductManagement.DAL.Interfaces;
using ProductManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Dto.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

    }
}

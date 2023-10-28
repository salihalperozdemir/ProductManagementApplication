using Azure;
using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Interfaces;
using ProductManagement_DAL.Data;
using System.Linq.Expressions;

namespace ProductManagement.DAL.Repositories
{
    //Generic Repository Implementation for using every repository for general purposes
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> _dbSet = null;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

		public T Get(Expression<Func<T, bool>> predicate)
		{
			return _dbSet.FirstOrDefault(predicate);
		}

		public T Add(T entity)
        {
            var response = _dbSet.Add(entity);
			_dbContext.SaveChanges();
			return response.Entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                return;
            Delete(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
			_dbContext.SaveChanges();
		}

        public T Update(T entity)
        {
            _dbSet.Update(entity);
			_dbContext.SaveChanges();
			return entity;
		}

    }
}

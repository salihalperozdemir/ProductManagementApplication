using ProductManagement.Business.Models.ResponseModels;
using ProductManagement.Core.Model;
using ProductManagement.Dto.Interfaces;
using ProductManagement.Entities.Models;

namespace ProductManagement.Business.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {

            _categoryRepository = categoryRepository;
        }
        //List categories for selectbox data
        public List<KeyValue> GetCategoryList()
        {
            var listCategories = new List<KeyValue>();
            try
            {
                listCategories = _categoryRepository.GetAll().Select(x => new KeyValue { Key = x.CategoryId, Value = x.Name }).ToList();

                return listCategories;
            }
            catch (Exception ex)
            {
                return listCategories;
            }
           
        }
        //Get category detail
        public CategoryResponse GetCategory(int categoryId)
        {
            var response = new CategoryResponse();
            try
            {
                var category = _categoryRepository.GetById(categoryId);

                if (category != null)
                {
                    response.CategoryId = category.CategoryId;
                    response.Name = category.Name;
                    response.IsOk = true;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Category not found";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
        }
        //Create new category
        public CategoryResponse AddCategory(Category category)
        {
            var response = new CategoryResponse { IsOk = true };
            try
            {
                var categoryResponse = _categoryRepository.Add(category);

                if (categoryResponse != null)
                {
                    response.IsOk = true;
                    response.CategoryId = categoryResponse.CategoryId;
                    response.Name = categoryResponse.Name;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Category creating is unsuccesfull";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
        }
        //Update category
        public CategoryResponse UpdateCategory(Category category)
        {
            var response = new CategoryResponse { IsOk = true };
            if (category.CategoryId == 0)
            {
                response.IsOk = false;
                response.Message = "Category is not found";
            }
            try
            {
                var categoryExist = _categoryRepository.GetById(category.CategoryId);
                if (categoryExist == null)
                {
                    response.IsOk = false;
                    response.Message = "Category is not found";
                }
                var categoryResponse = _categoryRepository.Update(category);

                if (categoryResponse != null)
                {
                    response.IsOk = true;
                    response.CategoryId = categoryResponse.CategoryId;
                    response.Name = categoryResponse.Name;
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Category updating is unsuccesfull";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }

        }
        //Get all categories for listing
        public List<Category> GetCategories()
        {
            var categoryList = new List<Category>();
           
            try
            {
                categoryList = _categoryRepository.GetAll().ToList();
                return categoryList;
            }
            catch (Exception ex)
            {
                return categoryList;
            }
           
        }
        //Delete category from system
        public CategoryResponse DeleteCategory(int categoryId)
        {
            var response = new CategoryResponse { IsOk = false };
            try
            {
                var category = _categoryRepository.GetById(categoryId);
                if (category == null)
                {
                    response.IsOk = false;
                    response.Message = "Category is not found";
                    return response;
                }

                _categoryRepository.Delete(category);

                response.IsOk = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}

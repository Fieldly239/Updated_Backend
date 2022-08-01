using KM_Management_Api.Models;
using KM_Management_Api.Repositories;

namespace KM_Management_Api.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<bool> Add(Category model);
        Task<bool> Update(Category model);
        Task<bool> Delete(int id);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryRepository.GetById(id);
        }
        public async Task<bool> Add(Category model)
        {
            
            var categoryList = await _categoryRepository.GetAll();
            var isDupicate = categoryList.Where(m => m.Name == model.Name);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error Category is Dupicate");
            }
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            return await _categoryRepository.Add(model) > 0;
        }

        public async Task<bool> Update(Category model)
        {
            var categoryList = await _categoryRepository.GetAll();
            var isDupicate = categoryList.Where((m) => m.Name == model.Name && m.Id != model.Id);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error Category is Dupicate");
            }
            model.ModifiedDate = DateTime.Now;
            return await _categoryRepository.Update(model) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new Exception("Error Not Found");
            }
            return await _categoryRepository.Delete(id) > 0;
        }

        
    }
}

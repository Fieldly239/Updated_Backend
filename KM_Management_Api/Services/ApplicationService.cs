using KM_Management_Api.Models;
using KM_Management_Api.Repositories;

namespace KM_Management_Api.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetAll();
        Task<Application> GetById(int id);
        Task<bool> Add(Application model);
        Task<bool> Update(Application model);
        Task<bool> Delete(int id);
    }

    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<IEnumerable<Application>> GetAll()
        {
            return await _applicationRepository.GetAll();
        }

        public async Task<Application> GetById(int id)
        {
            return await _applicationRepository.GetById(id);
        }
        public async Task<bool> Add(Application model)
        {
            
            
            var applicationList = await _applicationRepository.GetAll();
            var isDupicate = applicationList.Where(m => m.Name == model.Name);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error Application is Dupicate");
            }
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            return await _applicationRepository.Add(model) > 0;
        }

        public async Task<bool> Update(Application model)
        {
            var applicationList = await _applicationRepository.GetAll();
            var isDupicate = applicationList.Where((m) => m.Name == model.Name && m.Id != model.Id);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error Application is Dupicate");
            }
            model.ModifiedDate = DateTime.Now;
            return await _applicationRepository.Update(model) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var application = await _applicationRepository.GetById(id);
            if (application == null)
            {
                throw new Exception("Error Not Found");
            }
            return await _applicationRepository.Delete(id) > 0;
        }

    }
}

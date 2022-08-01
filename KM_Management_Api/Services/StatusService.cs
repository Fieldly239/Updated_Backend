using KM_Management_Api.Models;
using KM_Management_Api.Repositories;

namespace KM_Management_Api.Services
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetAll();

        Task<Status> GetById(int id);
        Task<bool> Add(Status status);
        Task<bool> Update(Status status);
        Task<bool> Delete(int id);
    }
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<Status>> GetAll()
        {
            var status = await _statusRepository.GetAll();
            var resp = status.OrderByDescending(m => m.Name);
            return resp;
        }

        public async Task<Status> GetById(int id)
        {
            return await _statusRepository.GetById(id);
        }

        public async Task<bool> Add(Status status)
        {
            var statusList = await _statusRepository.GetAll();
            var isDupicate = statusList.Where(m => m.Name == status.Name);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error Status is Dupicate");
            }
            status.CreatedDate= DateTime.Now;
            status.ModifiedDate= DateTime.Now;
            return await _statusRepository.Add(status) > 0;
        }

        public async Task<bool> Update(Status status)
        {
            var statusList = await _statusRepository.GetAll();
            var isDupicate = statusList.Where(m => m.Name == status.Name && m.Id != status.Id);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error Status is Dupicate");
            }
            status.ModifiedDate = DateTime.Now;
            return await _statusRepository.Update(status) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _statusRepository.GetById(id);
            if (role == null)
            {
                throw new Exception("Error Status not exist");
            }
            return await _statusRepository.Delete(id) > 0;
        }

    }
}

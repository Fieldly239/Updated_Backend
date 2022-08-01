using KM_Management_Api.Models;
using KM_Management_Api.Repositories;

namespace KM_Management_Api.Services
{
    public interface IKMService
    {
        Task<IEnumerable<KM>> GetAll();

        Task<IEnumerable<KM>> GetByApplication(int id);

        Task<IEnumerable<KM>> GetByCategory(int id);

        Task<IEnumerable<KM>> GetByStatus(int id);

        Task<IEnumerable<KM>> GetSearchAll(KMSearchAll model);

        Task<IEnumerable<KMTopListView>> GetTopKnowLedge();

        Task<KM> GetById(int id);
        Task<bool> Add(KM model);
        Task<bool> Update(KM model);
        Task<bool> Delete(int id);
    }

    public class KMService : IKMService
    {
        private readonly IKMRepository _kmRepository;

        public KMService(IKMRepository kmRepository)
        {
            _kmRepository = kmRepository;
        }

        public async Task<IEnumerable<KM>> GetAll()
        {
            return await _kmRepository.GetAll();
          
        }

        public async Task<KM> GetById(int id)
        {
            return await _kmRepository.GetById(id);
        }

        public async Task<IEnumerable<KM>> GetByApplication(int id)
        {
            return await _kmRepository.GetByApplication(id);
        }

        public async Task<IEnumerable<KM>> GetByCategory(int id)
        {
            return await _kmRepository.GetByCategory(id);
        }

        public async Task<IEnumerable<KM>> GetByStatus(int id)
        {
            return await _kmRepository.GetByStatus(id);
        }

        public async Task<bool> Add(KM model)
        {
            
            var kmList = await _kmRepository.GetAll();
            var isDupicate = kmList.Where(m => m.KeyName == model.KeyName && m.Title == model.Title);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error KM is Dupicate");
            }
            model.KeyName = Guid.NewGuid().ToString();
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            return await _kmRepository.Add(model) > 0;
        }

        public async Task<bool> Update(KM model)
        {
            var kmList = await _kmRepository.GetAll();
            var isDupicate = kmList.Where((m) => m.KeyName == model.KeyName && m.Title == model.Title && m.Id != model.Id);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error KM is Dupicate");
            }
            
            model.ModifiedDate = DateTime.Now;
            return await _kmRepository.Update(model) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var km = await _kmRepository.GetById(id);
            if (km == null)
            {
                throw new Exception("Error Not Found");
            }
            return await _kmRepository.Delete(id) > 0;
        }

        public async Task<IEnumerable<KM>> GetSearchAll(KMSearchAll model)
        {
            var searchList = await _kmRepository.GetAll();

            if (!string.IsNullOrEmpty(model.Title))
                searchList = searchList.Where((m) => m.Title.ToLower().Contains(model.Title.ToLower())).ToList();

            if (model.FK_ApplicationId != null && model.FK_ApplicationId != 0)
                searchList = searchList.Where(m => m.FK_ApplicationId == model.FK_ApplicationId).ToList();

            if (model.FK_CategoryId != null && model.FK_CategoryId != 0)
                searchList = searchList.Where((m) => m.FK_CategoryId == model.FK_CategoryId).ToList();

            var resp = searchList.OrderByDescending(k => k.ModifiedDate);
            return resp.ToList();
        }

        public async Task<IEnumerable<KMTopListView>> GetTopKnowLedge()
        {

            var knowledge = await _kmRepository.GetTopKnowLedge();

            var topKnowledge = knowledge.OrderByDescending(k => k.ModifiedDate).Take(100).ToList();


            var resp = topKnowledge;
            return resp.ToList();
        }
    }
}

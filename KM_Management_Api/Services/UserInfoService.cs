using KM_Management_Api.Models;
using KM_Management_Api.Repositories;

namespace KM_Management_Api.Services
{
    public interface IUserInfoService
    {
        Task<IEnumerable<UserInfo>> GetAll();

        Task<UserInfo> GetById(int id);
        Task<bool> Add(UserInfo userInfo);
        Task<bool> Update(UserInfo userInfo);
        Task<bool> Delete(int id);
    }
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public UserInfoService(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public async Task<IEnumerable<UserInfo>> GetAll()
        {
            var userInfo = await _userInfoRepository.GetAll();
            var resp = userInfo.OrderByDescending(m => m.EmpId);
            return resp;
        }

        public async Task<UserInfo> GetById(int id)
        {
            return await _userInfoRepository.GetById(id);
        }

        public async Task<bool> Add(UserInfo userInfo)
        {
            var userInfoList = await _userInfoRepository.GetAll();
            var isDupicate = userInfoList.Where(m => m.EmpId == userInfo.EmpId);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error UserInfo is Dupicate");
            }
            return await _userInfoRepository.Add(userInfo) > 0;
        }

        public async Task<bool> Update(UserInfo userInfo)
        {
            var userInfoList = await _userInfoRepository.GetAll();
            var isDupicate = userInfoList.Where(m => m.EmpId == userInfo.EmpId && m.Id != userInfo.Id);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error UserInfo is Dupicate");
            }
            return await _userInfoRepository.Update(userInfo) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var userInfo = await _userInfoRepository.GetById(id);
            if (userInfo == null)
            {
                throw new Exception("Error UserInfo not exist");
            }
            return await _userInfoRepository.Delete(id) > 0;
        }

    }
}

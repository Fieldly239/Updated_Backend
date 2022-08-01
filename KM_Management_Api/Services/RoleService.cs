using KM_Management_Api.Models;
using KM_Management_Api.Repositories;

namespace KM_Management_Api.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll();

        Task<Role> GetById(int id);
        Task<bool> Add(Role role);
        Task<bool> Update(Role role);
        Task<bool> Delete(int id);
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            var role = await _roleRepository.GetAll();
            var resp = role.OrderByDescending(m => m.KeyName);
            return resp;
        }

        public async Task<Role> GetById(int id)
        {
            return await _roleRepository.GetById(id);
        }

        public async Task<bool> Add(Role role)
        {
            var roleList = await _roleRepository.GetAll();
            var isDupicate = roleList.Where(m => m.KeyName == role.KeyName);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error Role is Dupicate");
            }
            return await _roleRepository.Add(role) > 0;
        }

        public async Task<bool> Update(Role role)
        {
            var roleList = await _roleRepository.GetAll();
            var isDupicate = roleList.Where(m => m.KeyName == role.KeyName && m.Id != role.Id);
            if (isDupicate.Count() > 0)
            {
                throw new Exception("Error Role is Dupicate");
            }
            return await _roleRepository.Update(role) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _roleRepository.GetById(id);
            if (role == null)
            {
                throw new Exception("Error Role not exist");
            }
            return await _roleRepository.Delete(id) > 0;
        }

    }
}

using SampleProject.Common.Infrastructure.Helper;
using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.ResponseModels;
using SampleProject.Data.Interfaces;
using SampleProject.Service.Interfaces;

namespace SampleProject.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="roleRepository"></param>
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// create a role if not already exists
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<Guid?> CreateRoleIfNotExists(int roleId, string roleName)
        {
            var model = new RoleModel { RoleId = roleId, RoleName = roleName, RoleGuid = Guid.NewGuid(), CreatedDateUtc = DateTime.UtcNow };

            var result = await _roleRepository.CreateRoleIfNotExists(model);

            return result > 0 ? model.RoleGuid : null;
        }

        /// <summary>
        /// get list of all roles
        /// </summary>
        /// <returns> list of roles </returns>
        public async Task<ServiceResult<IEnumerable<RoleViewModel>>> GetAllRoles()
        {
            var serviceResult = new ServiceResult<IEnumerable<RoleViewModel>>();
            try
            {
                var usersList = await _roleRepository.GetAllRoles();

                serviceResult.SetData(usersList);
            }
            catch (Exception ex)
            {
                serviceResult.SetError(!string.IsNullOrEmpty(ex.Message) ? ex.Message : ex.InnerException.Message);
            }
            return serviceResult;
        }
    }
}

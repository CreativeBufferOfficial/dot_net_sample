using SampleProject.Common.Infrastructure.Helper;
using SampleProject.Common.Infrastructure.Models.ResponseModels;

namespace SampleProject.Service.Interfaces
{
    public interface IRoleService
    {
        Task<Guid?> CreateRoleIfNotExists(int roleId, string roleName);
        Task<ServiceResult<IEnumerable<RoleViewModel>>> GetAllRoles();
    }
}

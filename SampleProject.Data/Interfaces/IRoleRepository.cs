using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.ResponseModels;

namespace SampleProject.Data.Interfaces
{
    public interface IRoleRepository
    {
        Task<int> CreateRoleIfNotExists(RoleModel model);
        Task<IEnumerable<RoleViewModel>> GetAllRoles();
    }
}

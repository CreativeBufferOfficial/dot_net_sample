using SampleProject.Common.Infrastructure.Helper;
using SampleProject.Common.Infrastructure.Models.RequestModels.User;
using SampleProject.Common.Infrastructure.Models.ResponseModels;

namespace SampleProject.Service.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<Guid>> CreateUser(CreateUserRequestModel model, int userId);
        Task<ServiceResult<LoginResponseModel>> AuthenticateUser(LoginRequestModel model);
        Task<ServiceResult<IEnumerable<UserViewModel>>> GetAllUsers(bool? showDeleted);
        Task<ServiceResult<bool>> UpdateUser(UpdateUserRequestModel model, int userId);
        Task<ServiceResult<UserViewModel>> GetUserDetailsByGuid(Guid userGuid);
        Task<ServiceResult<bool>> DeleteUser(Guid userGuid, int userId);
    }
}

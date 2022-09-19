using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.ResponseModels;

namespace SampleProject.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<UserViewModel> GetUserDetailsByEmail(string email);
        Task<bool> CheckUserExistence(string email);
        Task<int> CreateUser(UserModel model);
        Task<IEnumerable<UserViewModel>> GetAllUsers(bool? showDeleted);
        Task<int> UpdateUser(UserModel model);
        Task<UserViewModel> GetUserDetailsByGuid(Guid userGuid);
        Task<int> DeleteUser(UserModel userModel);
    }
}

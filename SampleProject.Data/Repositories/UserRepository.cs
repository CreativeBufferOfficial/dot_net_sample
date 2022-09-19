using Dapper;
using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.ResponseModels;
using SampleProject.Data.Interfaces;
using SampleProject.Data.SqlConstants;
using System.Data;

namespace SampleProject.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="dbConnection"></param>
        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// authenticate whether the user is valid or not
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passwordHash"></param>
        /// <returns>
        /// the user details if the user is valid
        /// </returns>
        public async Task<UserViewModel> GetUserDetailsByEmail(string email)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<UserViewModel>(UserSqlConstants.GET_USER_DETAILS_BY_EMAIL,
                new { Email = email }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// checks whether a user exists with given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserExistence(string email)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(UserSqlConstants.CHECK_USER_EXISTENCE,
                new { Email = email }, commandType: CommandType.StoredProcedure) > 0;
        }

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<int> CreateUser(UserModel model)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(UserSqlConstants.CREATE_USER,
                new
                {
                    model.UserGuid,
                    model.FirstName,
                    model.LastName,
                    model.PhoneNumber,
                    model.Email,
                    model.PasswordHash,
                    model.RoleId,
                    model.IsActive,
                    model.IsDeleted,
                    model.CreatedBy,
                    model.CreatedDateUtc
                }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// get all users list
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserViewModel>> GetAllUsers(bool? showDeleted)
        {
            return await _dbConnection.QueryAsync<UserViewModel>(UserSqlConstants.GET_ALL_USERS,
                new { ShowDeleted = showDeleted }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// update user details
        /// </summary>
        /// <param name="model"></param>
        /// <returns> number of records affected </returns>
        public async Task<int> UpdateUser(UserModel model)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(UserSqlConstants.UPDATE_USER,
                new
                {
                    model.UserGuid,
                    model.FirstName,
                    model.LastName,
                    model.PhoneNumber,
                    model.RoleId,
                    model.IsActive,
                    model.ModifiedBy,
                    model.ModifiedDateUtc
                }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// get user details by user guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<UserViewModel> GetUserDetailsByGuid(Guid userGuid)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<UserViewModel>(UserSqlConstants.GET_USER_DETAILS_BY_GUID,
                new { UserGuid = userGuid }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// soft delete an existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns> number of records affected </returns>
        public async Task<int> DeleteUser(UserModel model)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(UserSqlConstants.DELETE_USER,
                new
                {
                    model.UserGuid,
                    model.ModifiedBy,
                    model.ModifiedDateUtc
                }, commandType: CommandType.StoredProcedure);
        }
    }
}

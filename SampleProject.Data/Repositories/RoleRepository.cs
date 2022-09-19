using Dapper;
using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.ResponseModels;
using SampleProject.Data.Interfaces;
using SampleProject.Data.SqlConstants;
using System.Data;

namespace SampleProject.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="dbConnection"></param>
        public RoleRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// create a role if not already exists
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> CreateRoleIfNotExists(RoleModel model)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(RoleSqlConstants.CREATE_ROLE_IF_NOT_EXISTS, model,
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// get list of all roles
        /// </summary>
        /// <returns> list of roles </returns>
        public async Task<IEnumerable<RoleViewModel>> GetAllRoles()
        {
            return await _dbConnection.QueryAsync<RoleViewModel>(RoleSqlConstants.GET_ALL_ROLES,
                commandType: CommandType.StoredProcedure);
        }
    }
}

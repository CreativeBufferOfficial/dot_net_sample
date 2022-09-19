using Dapper;
using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Data.Interfaces;
using SampleProject.Data.SqlConstants;
using System.Data;

namespace SampleProject.Data.Repositories
{
    public class ExceptionLogRepository : IExceptionLogRepository
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="dbConnection"></param>
        public ExceptionLogRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// creates a new entry in the Exception Log 
        /// </summary>
        /// <param name="exceptionModel"></param>
        /// <returns></returns>
        public async Task CreateExceptionLog(ExceptionModel exceptionModel)
        {
            await _dbConnection.ExecuteAsync(ExceptionLogSqlConstants.CREATE_EXCEPTION_LOG, exceptionModel,
                commandType: CommandType.StoredProcedure);
        }
    }
}

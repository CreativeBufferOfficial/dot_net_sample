using Dapper;
using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.ResponseModels;
using SampleProject.Data.Interfaces;
using SampleProject.Data.SqlConstants;
using System.Data;

namespace SampleProject.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="dbConnection"></param>
        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// create a new customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns> the newly generated customer id, if created successfully</returns>
        public async Task<int> CreateCustomer(CustomerModel customerModel)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(CustomerSqlConstants.CREATE_CUSTOMER,
                new
                {
                    customerModel.CustomerGuid,
                    customerModel.CustomerName,
                    customerModel.PhoneNumber,
                    customerModel.Email,
                    customerModel.Address,
                    customerModel.City,
                    customerModel.State,
                    customerModel.Country,
                    customerModel.Zip,
                    customerModel.IsDeleted,
                    customerModel.CreatedBy,
                    customerModel.CreatedDateUtc
                }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// soft delete an existing customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns> number of records affected </returns>
        public async Task<int> DeleteCustomer(CustomerModel model)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(CustomerSqlConstants.DELETE_CUSTOMER,
                new
                {
                    model.CustomerGuid,
                    model.ModifiedBy,
                    model.ModifiedDateUtc
                }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// get all customers list
        /// </summary>
        /// <param name="showDeleted"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomers(bool? showDeleted)
        {
            return await _dbConnection.QueryAsync<CustomerViewModel>(CustomerSqlConstants.GET_ALL_CUSTOMERS,
                new { ShowDeleted = showDeleted }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// get customer details by user guid
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <returns></returns>
        public async Task<CustomerViewModel> GetCustomerDetailsByGuid(Guid customerGuid)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<CustomerViewModel>(CustomerSqlConstants.GET_CUSTOMER_DETAILS_BY_GUID,
              new { CustomerGuid = customerGuid }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// update an existing customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns> the number of records affected </returns>
        public async Task<int> UpdateCustomer(CustomerModel customerModel)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(CustomerSqlConstants.UPDATE_CUSTOMER,
                new
                {
                    customerModel.CustomerGuid,
                    customerModel.CustomerName,
                    customerModel.PhoneNumber,
                    customerModel.Email,
                    customerModel.Address,
                    customerModel.City,
                    customerModel.State,
                    customerModel.Country,
                    customerModel.Zip,
                    customerModel.ModifiedBy,
                    customerModel.ModifiedDateUtc
                }, commandType: CommandType.StoredProcedure);
        }
    }
}

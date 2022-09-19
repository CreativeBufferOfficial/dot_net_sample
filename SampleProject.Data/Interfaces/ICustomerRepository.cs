using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.ResponseModels;

namespace SampleProject.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> CreateCustomer(CustomerModel customerModel);
        Task<int> DeleteCustomer(CustomerModel customerModel);
        Task<IEnumerable<CustomerViewModel>> GetAllCustomers(bool? showDeleted);
        Task<CustomerViewModel> GetCustomerDetailsByGuid(Guid customerGuid);
        Task<int> UpdateCustomer(CustomerModel customerModel);
    }
}

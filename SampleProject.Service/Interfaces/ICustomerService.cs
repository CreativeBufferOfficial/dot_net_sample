using SampleProject.Common.Infrastructure.Helper;
using SampleProject.Common.Infrastructure.Models.RequestModels.Customer;
using SampleProject.Common.Infrastructure.Models.ResponseModels;

namespace SampleProject.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResult<Guid>> CreateCustomer(CreateCustomerRequestModel model, int userId);
        Task<ServiceResult<bool>> DeleteCustomer(Guid customerGuid, int userId);
        Task<ServiceResult<IEnumerable<CustomerViewModel>>> GetAllCustomers(bool? showDeleted);
        Task<ServiceResult<CustomerViewModel>> GetCustomerDetailsByGuid(Guid customerGuid);
        Task<ServiceResult<bool>> UpdateCustomer(UpdateCustomerRequestModel model, int userId);
    }
}

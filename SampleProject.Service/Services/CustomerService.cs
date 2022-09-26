using AutoMapper;
using SampleProject.Common.Infrastructure.Helper;
using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.RequestModels.Customer;
using SampleProject.Common.Infrastructure.Models.ResponseModels;
using SampleProject.Data.Interfaces;
using SampleProject.Service.Interfaces;

namespace SampleProject.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="customerRepository"></param>
        public CustomerService(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// create a new customer
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns> the newly generated customer Guid, if created successfully </returns>
        public async Task<ServiceResult<Guid>> CreateCustomer(CreateCustomerRequestModel model, int userId)
        {
            var serviceResult = new ServiceResult<Guid>();

            var customerModel = _mapper.Map<CustomerModel>(model);
            customerModel.CreatedBy = userId;

            var result = await _customerRepository.CreateCustomer(customerModel);

            if (result > 0)
                serviceResult.SetSuccess(customerModel.CustomerGuid, "Customer created successfully");
            else
                serviceResult.SetError("Failed to create customer.");

            return serviceResult;
        }

        /// <summary>
        /// soft delete an existing customer
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// returns whether successfully deleted or not
        /// </returns>
        public async Task<ServiceResult<bool>> DeleteCustomer(Guid customerGuid, int userId)
        {
            var serviceResult = new ServiceResult<bool>();

            var result = await _customerRepository.DeleteCustomer(new CustomerModel { ModifiedBy = userId, ModifiedDateUtc = DateTime.UtcNow, CustomerGuid = customerGuid });

            if (result > 0)
                serviceResult.SetSuccess(result > 0, "Customer deleted successfully");
            else
                serviceResult.SetError("Failed to delete customer.");

            return serviceResult;
        }

        /// <summary>
        /// get all customers list
        /// </summary>
        /// <param name="showDeleted"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<CustomerViewModel>>> GetAllCustomers(bool? showDeleted)
        {
            var serviceResult = new ServiceResult<IEnumerable<CustomerViewModel>>();

            serviceResult.SetData(await _customerRepository.GetAllCustomers(showDeleted));

            return serviceResult;
        }

        /// <summary>
        /// get customer details by customer guid
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <returns></returns>
        public async Task<ServiceResult<CustomerViewModel>> GetCustomerDetailsByGuid(Guid customerGuid)
        {
            var serviceResult = new ServiceResult<CustomerViewModel>();

            var customerDetails = await _customerRepository.GetCustomerDetailsByGuid(customerGuid);

            if (customerDetails == null)
                serviceResult.SetError("No customer found with provided details");
            else
                serviceResult.SetData(customerDetails);

            return serviceResult;
        }

        /// <summary>
        /// update an existing customer
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// returns whether successfully deleted or not
        /// </returns>
        public async Task<ServiceResult<bool>> UpdateCustomer(UpdateCustomerRequestModel model, int userId)
        {
            var serviceResult = new ServiceResult<bool>();

            var existingCustomer = await _customerRepository.GetCustomerDetailsByGuid(model.CustomerGuid);

            if (existingCustomer == null)
                throw new Exception("No customer found with the provided data.");
            else
            {
                var customerModel = _mapper.Map<CustomerModel>(model);
                customerModel.ModifiedBy = userId;
                var result = await _customerRepository.UpdateCustomer(customerModel);

                if (result > 0)
                    serviceResult.SetSuccess(result > 0, "Customer updated successfully");
                else
                    serviceResult.SetError("Failed to update customer.");
            }

            return serviceResult;
        }
    }
}

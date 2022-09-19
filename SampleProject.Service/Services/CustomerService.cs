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
            try
            {
                var customerModel = _mapper.Map<CustomerModel>(model);
                customerModel.CreatedBy = userId;

                var result = await _customerRepository.CreateCustomer(customerModel);

                if (result > 0)
                    serviceResult.SetData(customerModel.CustomerGuid);
                else
                    throw new Exception("Failed to create customer.");
            }
            catch (Exception ex)
            {
                serviceResult.SetError(!string.IsNullOrEmpty(ex.Message) ? ex.Message : ex.InnerException.Message);
            }
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
            try
            {
                var result = await _customerRepository.DeleteCustomer(new CustomerModel { ModifiedBy = userId, ModifiedDateUtc = DateTime.UtcNow, CustomerGuid = customerGuid });

                if (result > 0)
                    serviceResult.SetData(result > 0);
                else
                    throw new Exception("Failed to delete customer.");
            }
            catch (Exception ex)
            {
                serviceResult.SetError(!string.IsNullOrEmpty(ex.Message) ? ex.Message : ex.InnerException.Message);
            }
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
            try
            {
                var customersList = await _customerRepository.GetAllCustomers(showDeleted);

                serviceResult.SetData(customersList);
            }
            catch (Exception ex)
            {
                serviceResult.SetError(!string.IsNullOrEmpty(ex.Message) ? ex.Message : ex.InnerException.Message);
            }
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
            try
            {
                var customerDetails = await _customerRepository.GetCustomerDetailsByGuid(customerGuid);

                if (customerDetails == null)
                    throw new Exception("No customer found with provided details");
                else
                    serviceResult.SetData(customerDetails);
            }
            catch (Exception ex)
            {
                serviceResult.SetError(!string.IsNullOrEmpty(ex.Message) ? ex.Message : ex.InnerException.Message);
            }
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
            try
            {
                var existingCustomer = await _customerRepository.GetCustomerDetailsByGuid(model.CustomerGuid);

                if (existingCustomer == null)
                    throw new Exception("No customer found with the provided data.");

                var customerModel = _mapper.Map<CustomerModel>(model);
                customerModel.ModifiedBy = userId;
                var result = await _customerRepository.UpdateCustomer(customerModel);

                if (result > 0)
                    serviceResult.SetData(result > 0);
                else
                    throw new Exception("Failed to update customer.");
            }
            catch (Exception ex)
            {
                serviceResult.SetError(!string.IsNullOrEmpty(ex.Message) ? ex.Message : ex.InnerException.Message);
            }
            return serviceResult;
        }
    }
}

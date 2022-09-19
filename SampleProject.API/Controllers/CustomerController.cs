using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Common.Infrastructure.Models.RequestModels.Customer;
using SampleProject.Service.Interfaces;

namespace SampleProject.API.Controllers
{
    /// <summary>
    /// conatins the API Endpoints related to Customer
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// get list of all customers
        /// </summary>
        /// <param name="showDeleted"></param>
        /// <returns>
        /// list of customers
        /// </returns>
        /// <remarks>
        /// GET <br />
        /// 1. /api/v1/user/ => returns the list of all customers <br /> 
        /// 2. /api/v1/user/true => returns the list of deleted customers <br /> 
        /// 3. /api/v1/user/false => returns the list of valid customers <br /> 
        /// </remarks>
        [HttpGet("All/{showDeleted?}")]
        public async Task<IActionResult> GetAllCustomers(bool? showDeleted)
        {
            var result = await _customerService.GetAllCustomers(showDeleted);
            return Ok(result);
        }

        /// <summary>
        /// get customer details by customer guid
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <returns> the customer details based on guid</returns>
        [HttpGet("{customerGuid}")]
        public async Task<IActionResult> GetCustomerDetailsByGuid(Guid customerGuid)
        {
            var result = await _customerService.GetCustomerDetailsByGuid(customerGuid);
            return Ok(result);
        }

        /// <summary>
        /// delete user based on customer guid
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <returns>   
        /// returns whether successfully deleted or not
        /// </returns>
        [HttpDelete("{customerGuid}")]
        public async Task<IActionResult> DeleteCustomer(Guid customerGuid)
        {
            var result = await _customerService.DeleteCustomer(customerGuid, UserId);
            return Ok(result);
        }

        /// <summary>
        /// create a new customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// returns the newly generated customer guid if successfully created
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequestModel model)
        {
            var result = await _customerService.CreateCustomer(model, UserId);
            return Ok(result);
        }

        /// <summary>
        /// update an existing customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// returns whether successfully updated or not
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerRequestModel model)
        {
            var result = await _customerService.UpdateCustomer(model, UserId);
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Common.Infrastructure;
using SampleProject.Common.Infrastructure.Models.RequestModels.User;
using SampleProject.Service.Interfaces;

namespace SampleProject.API.Controllers
{
    /// <summary>
    /// conatins the API Endpoints related to User
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// to authenticate user credentials
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// returns the JWT token along with the expiry time if the user is valid
        /// </returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser(LoginRequestModel model)
        {
            var result = await _userService.AuthenticateUser(model);
            return Ok(result);
        }

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// returns the newly generated user guid if successfully created
        /// </returns>
        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.Admin)]
        public async Task<IActionResult> CreateUser(CreateUserRequestModel model)
        {
            var result = await _userService.CreateUser(model, UserId);
            return Ok(result);
        }

        /// <summary>
        /// update an existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// returns whether successfully updated or not
        /// </returns>
        [HttpPut]
        [Authorize(Roles = GlobalConstants.Roles.Admin)]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestModel model)
        {
            var result = await _userService.UpdateUser(model, UserId);
            return Ok(result);
        }

        /// <summary>
        /// get list of all users
        /// </summary>
        /// <param name="showDeleted"></param>
        /// <returns>
        /// list of users
        /// </returns>
        /// <remarks>
        /// GET <br />
        /// 1. /api/v1/user/ => returns the list of all users <br /> 
        /// 2. /api/v1/user/true => returns the list of deleted users <br /> 
        /// 3. /api/v1/user/false => returns the list of valid users <br /> 
        /// </remarks>
        [HttpGet("All/{showDeleted?}")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers(bool? showDeleted)
        {
            var result = await _userService.GetAllUsers(showDeleted);
            return Ok(result);
        }

        /// <summary>
        /// get user details by user guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns> the user details based on guid</returns>
        [HttpGet("{userGuid}")]
        [Authorize(Roles = GlobalConstants.Roles.Admin)]
        public async Task<IActionResult> GetUserDetailsByGuid(Guid userGuid)
        {
            var result = await _userService.GetUserDetailsByGuid(userGuid);
            return Ok(result);
        }

        /// <summary>
        /// delete user based on user guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns>   
        /// returns whether successfully deleted or not
        /// </returns>
        [HttpDelete("{userGuid}")]
        [Authorize(Roles = GlobalConstants.Roles.Admin)]
        public async Task<IActionResult> DeleteUser(Guid userGuid)
        {
            var result = await _userService.DeleteUser(userGuid, UserId);
            return Ok(result);
        }
    }
}

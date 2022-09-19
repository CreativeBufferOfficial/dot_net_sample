using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Common.Infrastructure;
using SampleProject.Service.Interfaces;

namespace SampleProject.API.Controllers
{
    /// <summary>
    /// conatins the API Endpoints related to Role
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// get list of all roles
        /// </summary>
        /// <returns> list of roles </returns>
        [HttpGet("All")]
        [Authorize(Roles = GlobalConstants.Roles.Admin)]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRoles();
            return Ok(result);
        }
    }
}

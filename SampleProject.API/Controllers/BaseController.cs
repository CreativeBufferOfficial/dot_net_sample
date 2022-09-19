using Microsoft.AspNetCore.Mvc;
using SampleProject.Common.Infrastructure;

namespace SampleProject.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Current User Id
        /// </summary>
        public int UserId => int.Parse(User.Claims.FirstOrDefault(c => c.Type == GlobalConstants.TokenClaims.Id)?.Value);

    }
}

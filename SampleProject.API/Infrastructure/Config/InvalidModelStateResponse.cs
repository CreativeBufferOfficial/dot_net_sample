using Microsoft.AspNetCore.Mvc;
using SampleProject.API.Infrastructure.Extensions;

namespace SampleProject.API.Infrastructure.Config
{
    public static class InvalidModelStateResponse
    {
        /// <summary>
        /// This is used to produce error response if model state have errors
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            return new BadRequestObjectResult(errors.ToArray());
        }
    }
}

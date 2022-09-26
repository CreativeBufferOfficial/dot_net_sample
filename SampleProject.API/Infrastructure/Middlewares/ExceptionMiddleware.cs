using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SampleProject.Service.Interfaces;
using System.Data.SqlClient;
using System.Net;

namespace SampleProject.API.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IExceptionLogService _exceptionLogService;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="exceptionLogService"></param>
        public ExceptionMiddleware(RequestDelegate next, IExceptionLogService exceptionLogService)
        {
            this.next = next;
            _exceptionLogService = exceptionLogService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handle Exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            // save occured exception in our DB
            await _exceptionLogService.SaveError(exception, !string.IsNullOrEmpty(context.User.Identity?.Name) ? int.Parse(context.User.Identity.Name) : null);

            if (exception is DbUpdateException || exception is SqlException)
            {
                await BuildResponse(context, HttpStatusCode.BadRequest, exception.Message);
            }
            else if (exception is ApplicationException)
            {
                await BuildResponse(context, HttpStatusCode.NotFound, exception.Message);
            }
            else
            {
                await BuildResponse(context, HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        /// <summary>
        /// Build Global Exception Response. 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private async Task BuildResponse(HttpContext context, HttpStatusCode statusCode, string error)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}
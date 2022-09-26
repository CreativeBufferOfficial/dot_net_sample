using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Data.Interfaces;
using SampleProject.Service.Interfaces;

namespace SampleProject.Service.Services
{
    public class ExceptionLogService : IExceptionLogService
    {
        private readonly IExceptionLogRepository _exceptionLogRepository;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="exceptionLogRepository"></param>
        public ExceptionLogService(IExceptionLogRepository exceptionLogRepository)
        {
            _exceptionLogRepository = exceptionLogRepository;
        }


        /// <summary>
        /// log the generated exception in database
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="userId"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public async Task SaveError(Exception exception, int? userId)
        {
            await _exceptionLogRepository.CreateExceptionLog(new ExceptionModel
            {
                OccuredDateUtc = DateTime.UtcNow,
                UserId = userId,
                Message = string.IsNullOrEmpty(exception.Message) ? exception.InnerException.Message : exception.Message,
                Source = exception.Source,
                StackTrace = exception.StackTrace
            });
        }
    }
}

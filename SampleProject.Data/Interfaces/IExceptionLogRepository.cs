using SampleProject.Common.Infrastructure.Models.Entities;

namespace SampleProject.Data.Interfaces
{
    public interface IExceptionLogRepository
    {
        Task CreateExceptionLog(ExceptionModel exceptionModel);
    }
}

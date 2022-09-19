namespace SampleProject.Service.Interfaces
{
    public interface IExceptionLogService
    {
        Task SaveError(Exception exception, int? userId);
    }
}

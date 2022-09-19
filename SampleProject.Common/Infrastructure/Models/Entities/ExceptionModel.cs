namespace SampleProject.Common.Infrastructure.Models.Entities
{
    public class ExceptionModel
    {
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public DateTime OccuredDateUtc { get; set; }
        public int? UserId { get; set; }
    }
}

namespace SampleProject.Common.Infrastructure.Helper
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public bool HasError { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void SetData(T data)
        {
            HasError = false;
            Data = data;
        }

        public void SetSuccess(T data, string successMessage = "")
        {
            HasError = false;
            Data = data;
            SuccessMessage = successMessage;
        }

        public void SetError(string errorMessage, int errorCode = 0)
        {
            HasError = true;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }
    }
}

using System.Net;

namespace AzureStorageTableOperations.Models
{
    public class ServiceResponse<T>
    {
        public bool Failed { get; set; }
        public T ResultData { get; set; }
        public ErrorResponse ErrorData { get; set; }

        public void CreateSuccessResponse(T data)
        {
            Failed = false;
            ResultData = data;
        }
        public void CreateErrorResponse(HttpStatusCode errorCode, string errorMessage)
        {
            Failed = true;
            ErrorData = new ErrorResponse() { ErrorCode = errorCode, ErrorMessage = errorMessage };
        }
    }

    public class ErrorResponse
    {
        public HttpStatusCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

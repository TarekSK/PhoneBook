using System.Net;

namespace Domain.Helper
{
    public class ServiceResponse
    {
        public ServiceResponse() { }

        public ServiceResponse(HttpStatusCode statusCode) { }

        public ServiceResponse(HttpStatusCode statusCode, List<string> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public List<string> Errors { get; set; } = new List<string>() { };
    }
    public class ServiceResponse<TData> : ServiceResponse
    {
        public ServiceResponse() { }

        public TData Data { get; set; }

        public ServiceResponse(HttpStatusCode statusCode, TData data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        public ServiceResponse(HttpStatusCode statusCode, TData data, List<string> errors) {
        
            StatusCode = statusCode;
            Data = data;
            Errors = errors;
        }
    }
}

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class Response<T> : ResponseLess
    {
        public T? Data { get; set; }

        public Response(bool success, string? message = null, IDictionary<string, string[]>? validationErrors = null)
        {
            Success = success;
            Message = message;
            ValidationErrors = validationErrors;
        }

        public Response(T? data, string? message, bool success)
        {
            Data = data;
            Message = message;
            Success = success;
        }
    }
    public class ResponseLess
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public IDictionary<string, string[]>? ValidationErrors { get; set; }

        public ResponseLess() { }

        public ResponseLess(bool success)
        {
            Success = success;
        }
        public ResponseLess(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class Response
    {
        public static Response<T> Success<T>() =>
            new(true);
        public static ResponseLess Success() =>
            new(true);
        public static Response<T> Success<T>(string message) =>
            new(true, message);
        public static Response<T> Success<T>(string message, T data) =>
            new(data, message, true);
        public static Response<T> Success<T>(T data) =>
            new(data, null, true);
        public static Response<T> Fail<T>(string message) =>
            new(false, message);
        public static Response<T> Fail<T>() =>
            new(false, null);
        public static ResponseLess Fail() =>
           new(false);
        public static ResponseLess Fail(string message) =>
           new(false, message);
        public static Response<T> FailValidation<T>(IDictionary<string, string[]> validationErrors) =>
            new(false, null, validationErrors);
    }
}

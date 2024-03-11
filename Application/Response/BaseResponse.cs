namespace SytleHub.Application.Response
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public T? Value { get; set; }
    }
}
namespace Application.Response
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
namespace Application.Exception.Response
{
    public class ExceptionResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }

}
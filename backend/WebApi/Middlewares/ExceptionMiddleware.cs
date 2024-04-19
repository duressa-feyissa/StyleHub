using System.Net;
using backend.Application.Exceptions;
using backend.Application.Response;
using Newtonsoft.Json;

namespace backend.WebApi.Middlewares
{
	public class ExceptionMiddleware(RequestDelegate next)
	{
		private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			HttpStatusCode statusCode;
			string result;

			switch (exception)
			{
				case BadRequestException badRequestException:
					statusCode = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(
						new BaseResponse<string> { Message = exception.Message }
					);
					break;
				case ValidationErrorException validationException:
					statusCode = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(validationException.Errors);
					break;
				case NotFoundException notFoundException:
					statusCode = HttpStatusCode.NotFound;
					result = JsonConvert.SerializeObject(
						new BaseResponse<string> { Message = exception.Message }
					);
					break;
				case UnauthorizedAccessException unauthorizedException:
					statusCode = HttpStatusCode.Unauthorized;
					result = JsonConvert.SerializeObject(
						new BaseResponse<string> { Message = "Unauthorized access" }
					);
					break;
				default:
					statusCode = HttpStatusCode.InternalServerError;
					result = JsonConvert.SerializeObject(
						new BaseResponse<string>
						{
							Message = "An error occurred while processing your request."
						}
					);
					break;
			}

			context.Response.StatusCode = (int)statusCode;
			return context.Response.WriteAsync(result);
		}
	}
}

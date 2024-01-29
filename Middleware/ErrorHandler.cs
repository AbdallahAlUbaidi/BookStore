using System.Net;
using Newtonsoft.Json;
using Errors;

namespace Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
	private readonly RequestDelegate _next = next;

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleException(context, ex);
		}
	}

	private static async Task HandleException(HttpContext context, Exception exception)
	{
		var code = HttpStatusCode.InternalServerError;
		var response = new ErrorResponseDto { Success = false, Error = new ErrorDetailsDto() };

		Console.WriteLine(exception);

		response.Error.Code = 500;
		response.Error.Message = "Internal server error";

		if (exception is ApiError apiError)
		{
			code = (HttpStatusCode)apiError.HttpStatusCode;
			response.Error.Code = apiError.ErrorCode;
			response.Error.Message = apiError.Message;

			if (apiError is ValidationError validationError)
				response.Error.Issues = validationError.Issues;

		}

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)code;

		var result = JsonConvert.SerializeObject(response);
		await context.Response.WriteAsync(result);
	}
}

public class ErrorResponseDto
{
	public bool Success { get; set; }
	public required ErrorDetailsDto Error { get; set; }
}

public class ErrorDetailsDto
{
	public int? Code { get; set; }
	public string? Message { get; set; }
	public List<ValidationIssue>? Issues { get; set; }
}

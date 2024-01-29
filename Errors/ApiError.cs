
namespace Errors;

public abstract class ApiError(int httpStatus, string message, int ErrorCode) : Exception(message)
{
	public int HttpStatusCode { get; private set; } = httpStatus;
	public string ErrorMessage { get; set; } = message;
	public int ErrorCode { get; private set; } = ErrorCode;

}
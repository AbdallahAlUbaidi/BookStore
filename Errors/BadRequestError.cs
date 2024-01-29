
namespace Errors;

public class BadRequestError(string message) : ApiError(400, message, 1)
{

}
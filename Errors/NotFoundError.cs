
namespace Errors;

public class NotFoundError(string message) : ApiError(404, message, 2)
{

}
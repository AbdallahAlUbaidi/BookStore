namespace Errors;

public class DuplicateResourceError(string message) : ApiError(400, message, 4)
{

}
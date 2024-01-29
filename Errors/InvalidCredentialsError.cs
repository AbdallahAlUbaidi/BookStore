namespace Errors;

public class InvalidCredentialsError(string message) : ApiError(401, message, 3) { }
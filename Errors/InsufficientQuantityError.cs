namespace Errors;

public class InsufficientQuantityError(string message) : ApiError(400, message, 7) { }
namespace Errors;

public class InvalidGuidError(string message): ApiError(400, message, 6) {

}
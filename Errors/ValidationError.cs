namespace Errors;

public class ValidationIssue(string field, string message)
{
	public string Field { get; set; } = field;
	public string Message { get; set; } = message;
}

public class ValidationError(List<ValidationIssue> issues) : ApiError(400, "Invalid Data", 5)
{
	public List<ValidationIssue> Issues { get; set; } = issues;
}
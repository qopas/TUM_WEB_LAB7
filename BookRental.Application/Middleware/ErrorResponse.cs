namespace Application.Middleware;

public class ErrorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; }
    public IEnumerable<string> Errors { get; set; }
    public string? StackTrace { get; set; }
}
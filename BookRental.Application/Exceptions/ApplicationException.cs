namespace Application.Exceptions;

public class ApplicationException : Exception
{
    public IEnumerable<string> Errors { get; }

    public ApplicationException(string message) : base(message)
    {
        Errors = [message];
    }

    public ApplicationException(IEnumerable<string> errors) : base(string.Join(", ", errors))
    {
        Errors = errors;
    }
}
namespace Application.Exceptions;

public abstract class ApplicationException : Exception
{
    public IEnumerable<string> Errors { get; }

    protected ApplicationException(string message) : base(message)
    {
        Errors = [message];
    }

    protected ApplicationException(IEnumerable<string> errors) : base(string.Join(", ", errors))
    {
        Errors = errors;
    }
}
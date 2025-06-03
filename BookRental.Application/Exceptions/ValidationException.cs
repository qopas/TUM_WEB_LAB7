namespace Application.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException(IEnumerable<string> errors) : base(errors)
    {
    }
}
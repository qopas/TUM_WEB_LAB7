namespace Application.Exceptions;

public class BusinessLogicException : ApplicationException
{
    public BusinessLogicException(string message) : base(message)
    {
    }

    public BusinessLogicException(IEnumerable<string> errors) : base(errors)
    {
    }
}
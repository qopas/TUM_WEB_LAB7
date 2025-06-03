namespace BookRental.Domain.Exceptions;

public abstract class DomainException : Exception
{
    public IEnumerable<string> Errors { get; }

    public DomainException()
    {
    }
    public DomainException(string message, IEnumerable<string> errors) : base(message)
    {
        Errors = [message];
    }
}
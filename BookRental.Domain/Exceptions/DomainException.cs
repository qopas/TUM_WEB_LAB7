namespace BookRental.Domain.Exceptions;

public abstract class DomainException : Exception
{
    public IEnumerable<string> Errors { get; }

    protected DomainException(string message) : base(message)
    {
        Errors = [message];
    }
}
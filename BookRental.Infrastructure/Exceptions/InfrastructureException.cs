namespace BookRental.Infrastructure.Exceptions;

public abstract class InfrastructureException : Exception
{
    public IEnumerable<string> Errors { get; }

    protected InfrastructureException(string message) : base(message)
    {
        Errors = [message];
    }
}
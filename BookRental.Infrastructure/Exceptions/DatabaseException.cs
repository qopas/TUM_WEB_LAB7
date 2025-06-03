namespace BookRental.Infrastructure.Exceptions;

public class DatabaseException : InfrastructureException
{
    public DatabaseException(string message) : base(message)
    {
    }
}
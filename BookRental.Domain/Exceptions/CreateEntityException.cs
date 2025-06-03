using BookRental.Domain.Exceptions;

public class CreateEntityException : DomainException
{
    public CreateEntityException(string entityName, IEnumerable<string> errors) 
        : base($"{entityName} not possible to create entity", errors)
    {
    }
    
}
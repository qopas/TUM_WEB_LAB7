namespace BookRental.Domain.Exceptions;

public class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(string entityName, string id) 
        : base($"{entityName} with ID '{id}' was not found")
    {
    }

    public EntityNotFoundException(string entityName, IEnumerable<string> ids) 
        : base($"{entityName} with IDs '{string.Join(", ", ids)}' were not found")
    {
    }
}
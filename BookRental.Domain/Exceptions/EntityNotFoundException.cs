namespace BookRental.Domain.Exceptions;

public class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(string entityName, string id, IEnumerable<string> errors) 
        : base($"{entityName} with ID '{id}' was not found", errors)
    {
    }

    public EntityNotFoundException(string entityName, IEnumerable<string> ids, IEnumerable<string> errors) 
        : base($"{entityName} with IDs '{string.Join(", ", ids)}' were not found", errors)
    {
    }
}
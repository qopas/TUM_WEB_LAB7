using BookRental.Domain.Entities.Base;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;


public class Customer : FullAuditableEntity  
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    
    public string ApplicationUserId { get; private set; }
    public virtual ApplicationUser ApplicationUser { get; private set; }
    public virtual ICollection<Rent> Rents { get; private set; } 

    public static Result<Customer> Create(CustomerModel model)
    {
        var customer = new Customer
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = model.Address,
            City = model.City,
            ApplicationUserId = model.ApplicationUserId
        };

        return Result<Customer>.Success(customer);
    }
}

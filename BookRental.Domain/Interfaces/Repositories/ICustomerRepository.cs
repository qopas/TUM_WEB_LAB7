using BookRental.Domain.Entities;

namespace BookRental.Domain.Interfaces.Repositories;

public interface ICustomerRepository : IFullAuditableRepository<Customer>
{
}
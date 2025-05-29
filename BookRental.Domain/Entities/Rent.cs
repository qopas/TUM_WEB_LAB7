using BookRental.Domain.Entities.Base;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Enums;
using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

public class Rent : BaseEntity
{
    public DateTimeOffset RentDate { get; private set; }
    public DateTimeOffset DueDate { get; private set; }
    public DateTimeOffset? ReturnDate { get; private set; }
    public RentStatus Status { get; private set; }
    
    public string BookId { get; private set; }
    public virtual Book Book { get; private set; }
    public string CustomerId { get; private set; }
    public virtual Customer Customer { get; private set; }
    public string DestinationId { get; private set; }
    public virtual Destination Destination { get; private set; }

    public static Result<Rent> Create(RentModel model)
    {
        var rent = new Rent
        {
            RentDate = model.RentDate,
            DueDate = model.DueDate,
            ReturnDate = model.ReturnDate,
            Status = model.Status,
            BookId = model.BookId,
            CustomerId = model.CustomerId,
            DestinationId = model.DestinationId
        };

        return Result<Rent>.Success(rent);
    }
}

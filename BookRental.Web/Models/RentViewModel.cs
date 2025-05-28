using Application.Rent.Commands.CreateRent;
using Application.Rent.Commands.UpdateRent;
using Application.DTOs.Rent;
using BookRental.Domain.Enums;

namespace BookRental.Web.Models;

public class RentViewModel
{
    public string? Id { get; set; }
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTimeOffset RentDate { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public DateTimeOffset? ReturnDate { get; set; }
    public RentStatus Status { get; set; }
    public string BookTitle { get; set; }
    public string CustomerName { get; set; }
    public string DestinationName { get; set; }
    
    public static RentViewModel FromDto(RentDto dto)
    {
        return new RentViewModel
        {
            Id = dto.Id,
            BookId = dto.BookId,
            CustomerId = dto.CustomerId,
            DestinationId = dto.DestinationId,
            RentDate = dto.RentDate,
            DueDate = dto.DueDate,
            ReturnDate = dto.ReturnDate,
            Status = dto.Status,
            BookTitle = dto.BookTitle,
            CustomerName = dto.CustomerName,
            DestinationName = dto.DestinationName
        };
    }

    public CreateRentCommand ToCreateCommand()
    {
        return new CreateRentCommand
        {
            BookId = BookId,
            CustomerId = CustomerId,
            DestinationId = DestinationId,
            RentDate = RentDate,
            DueDate = DueDate
        };
    }
    
    public UpdateRentCommand ToUpdateCommand()
    {
        return new UpdateRentCommand
        {
            Id = Id,
            BookId = BookId,
            CustomerId = CustomerId,
            DestinationId = DestinationId,
            RentDate = RentDate,
            DueDate = DueDate,
            ReturnDate = ReturnDate,
            Status = Status
        };
    }
}
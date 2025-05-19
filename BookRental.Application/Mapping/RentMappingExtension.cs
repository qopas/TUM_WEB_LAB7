using Application.DTOs.Rent;
using BookRental.Domain.Entities;
using BookRental.Domain.Enums;

namespace Application.Mapping;

public static class RentMappingExtensions
{
    public static RentDto ToDto(this Rent rent)
    {
        return new RentDto
        {
            Id = rent.Id,
            BookId = rent.BookId,
            CustomerId = rent.CustomerId,
            DestinationId = rent.DestinationId,
            RentDate = rent.RentDate,
            DueDate = rent.DueDate,
            ReturnDate = rent.ReturnDate,
            Status = rent.Status,
            BookTitle = rent.Book?.Title,
            CustomerName = rent.Customer != null ? $"{rent.Customer.FirstName} {rent.Customer.LastName}" : null,
            DestinationName = rent.Destination?.Name
        };
    }

    public static IEnumerable<RentDto> ToDtoList(this IEnumerable<Rent> rents)
    {
        return rents.Select(rent => rent.ToDto());
    }

    public static Rent ToEntity(this CreateRentDto createRentDto)
    {
        return new Rent
        {
            BookId = createRentDto.BookId,
            CustomerId = createRentDto.CustomerId,
            DestinationId = createRentDto.DestinationId,
            RentDate = createRentDto.RentDate,
            DueDate = createRentDto.DueDate,
            Status = RentStatus.Active 
        };
    }

    public static Rent ToEntity(this UpdateRentDto updateRentDto, Rent existingRent)
    {
        existingRent.BookId = updateRentDto.BookId;
        existingRent.CustomerId = updateRentDto.CustomerId;
        existingRent.DestinationId = updateRentDto.DestinationId;
        existingRent.RentDate = updateRentDto.RentDate;
        existingRent.DueDate = updateRentDto.DueDate;
        existingRent.ReturnDate = updateRentDto.ReturnDate;
        existingRent.Status = updateRentDto.Status;
        
        return existingRent;
    }
}
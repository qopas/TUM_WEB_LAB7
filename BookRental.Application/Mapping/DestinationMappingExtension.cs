
using Application.DTOs.Destination;
using BookRental.Domain.Entities;
namespace Application.Mapping;

public static class DestinationMappingExtensions
{
    public static DestinationDto ToDto(this Destination destination)
    {
        return new DestinationDto
        {
            Id = destination.Id,
            Name = destination.Name,
            Address = destination.Address,
            City = destination.City,
            ContactPerson = destination.ContactPerson,
            PhoneNumber = destination.PhoneNumber
        };
    }

    public static IEnumerable<DestinationDto>ToDtoList(this IEnumerable<Destination> destinations)
    {
        return destinations.Select(destination => destination.ToDto());
    }

    public static Destination ToEntity(this CreateDestinationDto createDestinationDto)
    {
        return new Destination
        {
            Name = createDestinationDto.Name,
            Address = createDestinationDto.Address,
            City = createDestinationDto.City,
            ContactPerson = createDestinationDto.ContactPerson,
            PhoneNumber = createDestinationDto.PhoneNumber
        };
    }

    public static Destination ToEntity(this UpdateDestinationDto updateDestinationDto, Destination existingDestination)
    {
        existingDestination.Name = updateDestinationDto.Name;
        existingDestination.Address = updateDestinationDto.Address;
        existingDestination.City = updateDestinationDto.City;
        existingDestination.ContactPerson = updateDestinationDto.ContactPerson;
        existingDestination.PhoneNumber = updateDestinationDto.PhoneNumber;
        
        return existingDestination;
    }
}
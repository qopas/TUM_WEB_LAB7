using MediatR;

namespace BookRental.DTOs.In;

public interface IRequestIn<out TCommand> 
{
    TCommand Convert();
}
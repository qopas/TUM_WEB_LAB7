using System.ComponentModel.DataAnnotations;
using Application.Book.Queries.GetBookById;

namespace BookRental.DTOs.In.Book;

public class GetBookByIdInRequest : IRequestIn<GetBookByIdQuery>
{
    public string Id { get; set; }

    public GetBookByIdQuery Convert()
    {
        return new GetBookByIdQuery { Id = Id };
    }
}

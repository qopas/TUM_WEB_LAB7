using Application.Book.Queries.GetBooks;

namespace BookRental.DTOs.In.Book;

public class GetBooksInRequest : IRequestIn<GetBooksQuery>
{
    public GetBooksQuery Convert()
    {
        return new GetBooksQuery();
    }
}
using BookRental.DTOs.Out.Book;

namespace BookRental.DTOs.Out;

public interface IResponseOut<in T>
{
    object Convert(T result);
}
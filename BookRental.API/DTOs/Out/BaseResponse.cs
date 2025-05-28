namespace BookRental.DTOs.Out;

public class BaseResponse<T> : IResponseOut<T>
{
    public bool Success { get; init; }
    public string Message { get; init; }
    public T? Data { get; init; }

    public object Convert(T result)
    {
        return new BaseResponse<T>
        {
            Success = true,
            Message = "Operation completed successfully",
            Data = result
        };
    }
}

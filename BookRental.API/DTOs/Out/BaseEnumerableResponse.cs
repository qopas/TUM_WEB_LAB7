namespace BookRental.DTOs.Out;

public class BaseEnumerableResponse<T> : IResponseOut<IEnumerable<T>>
{
    public bool Success { get; init; }
    public string Message { get; init; } 
    public IEnumerable<T>? Data { get; init; }

    public object Convert(IEnumerable<T> result)
    {
        var dataList = result.ToList();
        return new BaseEnumerableResponse<T>
        {
            Success = true,
            Message = $"Retrieved {dataList.Count} items successfully",
            Data = dataList
        };
    }
}
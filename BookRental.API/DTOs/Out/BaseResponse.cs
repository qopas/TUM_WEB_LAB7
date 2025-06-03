namespace BookRental.DTOs.Out;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}

namespace BookRental.Domain.Common;

public static class ResultExtensions
{
    public static Result<bool> ToUpdateResult<T>(this int rowsAffected, string id)
    {
        var entityName = typeof(T).Name;
        return rowsAffected == 0
            ? Result<bool>.Failure([$"{entityName} with {id} not found"])
            : Result<bool>.Success(true);
    }
}
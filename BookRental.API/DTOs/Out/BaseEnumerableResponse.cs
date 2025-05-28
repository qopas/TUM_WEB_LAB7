namespace BookRental.DTOs.Out;

public class BaseEnumerableResponse<TItem, TDto> : IResponseOut<IEnumerable<TDto>>
    where TItem : IResponseOut<TDto>, new()
{
    private IEnumerable<TItem?> Items { get; set; } = new List<TItem>();

    public object Convert(IEnumerable<TDto> dtos)
    {
        Items = dtos.Select(dto =>
        {
            var item = new TItem();
            return (TItem)item.Convert(dto)!;
        });
        
        return Items;
    }
}
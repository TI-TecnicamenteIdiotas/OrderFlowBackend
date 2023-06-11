namespace NimbleFlow.Contracts.DTOs;

public class PaginatedDto<T>
{
    public int TotalItems { get; init; }
    public IEnumerable<T> Items { get; init; }

    public PaginatedDto(int totalItems, IEnumerable<T> items)
    {
        TotalItems = totalItems;
        Items = items;
    }
}
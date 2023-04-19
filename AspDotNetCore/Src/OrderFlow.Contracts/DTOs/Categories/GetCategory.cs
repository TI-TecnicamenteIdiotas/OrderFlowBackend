using OrderFlow.Data.Models;

namespace OrderFlow.Contracts.DTOs.Categories;

public class GetCategory
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int ColorTheme { get; set; }
    public int CategoryIcon { get; set; }

    public static GetCategory FromModel(Category category)
        => new()
        {
            Id = category.Id,
            Title = category.Title,
            ColorTheme = category.ColorTheme,
            CategoryIcon = category.CategoryIcon
        };
}
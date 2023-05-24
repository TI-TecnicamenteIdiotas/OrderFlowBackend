using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Categories;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int? ColorTheme { get; set; }
    public int? CategoryIcon { get; set; }

    public static CategoryDto FromModel(Category category)
        => new()
        {
            Id = category.Id,
            Title = category.Title,
            ColorTheme = category.ColorTheme,
            CategoryIcon = category.CategoryIcon
        };
}
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Categories;

public class PostCategory
{
    public string Title { get; set; } = null!;
    public int ColorTheme { get; set; }
    public int CategoryIcon { get; set; }

    public Category ToModel()
        => new()
        {
            Title = Title,
            ColorTheme = ColorTheme,
            CategoryIcon = CategoryIcon
        };
}
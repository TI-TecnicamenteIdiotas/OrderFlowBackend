using NimbleFlow.Contracts.Interfaces;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Categories;

public class CreateCategoryDto : IToModel<Category>
{
    public string Title { get; init; }
    public int? ColorTheme { get; init; }
    public int? CategoryIcon { get; init; }

    public CreateCategoryDto(string title)
    {
        Title = title;
    }

    public Category ToModel()
        => new()
        {
            Title = Title,
            ColorTheme = ColorTheme,
            CategoryIcon = CategoryIcon
        };
}
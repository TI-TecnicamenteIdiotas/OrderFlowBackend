namespace NimbleFlow.Data.Partials.DTOs;

public class CategoryDto
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public int? ColorTheme { get; set; }
    public int? CategoryIcon { get; set; }

    public CategoryDto(Guid id, string title)
    {
        Id = id;
        Title = title;
    }
}
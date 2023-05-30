namespace NimbleFlow.Data.Partials.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int? ColorTheme { get; set; }
    public int? CategoryIcon { get; set; }
}
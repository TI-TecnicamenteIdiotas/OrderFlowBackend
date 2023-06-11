using NimbleFlow.Data.Partials.DTOs;
using NimbleFlow.Data.Partials.Interfaces;

// ReSharper disable once CheckNamespace
namespace NimbleFlow.Data.Models;

public partial class Category : IIdentifiable<Guid>, ICreatedAtDeletedAt, IToDto<CategoryDto>
{
    public CategoryDto ToDto()
        => new(Id, Title)
        {
            ColorTheme = ColorTheme,
            CategoryIcon = CategoryIcon
        };
}

public partial class Product : IIdentifiable<Guid>, ICreatedAtDeletedAt, IToDto<ProductDto>
{
    public ProductDto ToDto()
        => new(Id, Title, CategoryId)
        {
            Description = Description,
            Price = Price,
            ImageUrl = ImageUrl,
            IsFavorite = IsFavorite
        };
}

public partial class Table : IIdentifiable<Guid>, ICreatedAtDeletedAt, IToDto<TableDto>
{
    public TableDto ToDto()
        => new(Id, Accountable)
        {
            IsFullyPaid = IsFullyPaid
        };
}
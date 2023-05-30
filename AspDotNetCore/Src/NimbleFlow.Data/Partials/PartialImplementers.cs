using NimbleFlow.Data.Partials.Dtos;
using NimbleFlow.Data.Partials.Interfaces;

// ReSharper disable once CheckNamespace
namespace NimbleFlow.Data.Models;

public partial class Category : IIdentifiable<Guid>, ICreatedAtDeletedAt, IToDto<CategoryDto>
{
    public CategoryDto ToDto()
        => new()
        {
            Id = Id,
            Title = Title,
            ColorTheme = ColorTheme,
            CategoryIcon = CategoryIcon
        };
}

public partial class Product : IIdentifiable<Guid>, ICreatedAtDeletedAt, IToDto<ProductDto>
{
    public ProductDto ToDto()
        => new()
        {
            Id = Id,
            Title = Title,
            Description = Description,
            Price = Price,
            ImageUrl = ImageUrl,
            IsFavorite = IsFavorite,
            CategoryId = CategoryId
        };
}

public partial class Table : IIdentifiable<Guid>, ICreatedAtDeletedAt, IToDto<TableDto>
{
    public TableDto ToDto()
        => new()
        {
            Id = Id,
            Accountable = Accountable,
            IsFullyPaid = IsFullyPaid
        };
}
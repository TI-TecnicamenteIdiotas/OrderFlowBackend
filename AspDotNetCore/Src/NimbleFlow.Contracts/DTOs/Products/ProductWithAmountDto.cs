using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Products;

public class ProductWithAmountDto : ProductDto
{
    public new Guid Id { get; set; }
    public int Amount { get; set; }

    public static ProductWithAmountDto FromModel(OrderProduct orderProduct) => new()
    {
        Id = orderProduct.Product.Id,
        Title = orderProduct.Product.Title,
        Description = orderProduct.Product.Description,
        Price = orderProduct.Product.Price,
        ImageUrl = orderProduct.Product.ImageUrl,
        IsFavorite = orderProduct.Product.IsFavorite,
        CategoryId = orderProduct.Product.CategoryId,
        Amount = orderProduct.ProductAmount
    };
}
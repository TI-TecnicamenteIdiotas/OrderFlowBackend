using System.Net;
using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Extensions;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services.Base;
using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Api.Services;

public class ProductService : ServiceBase<CreateProductDto, ProductDto, NimbleFlowContext, Product>
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository) : base(productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<HttpStatusCode> UpdateProductById(Guid productId, UpdateProductDto productDto)
    {
        var productEntity = await _productRepository.GetEntityById(productId);
        if (productEntity is null)
            return HttpStatusCode.NotFound;

        var shouldUpdate = false;
        if (productDto.Title.IsNotNullAndNotEquals(productEntity.Title))
        {
            productEntity.Title = productDto.Title ?? throw new NullReferenceException();
            shouldUpdate = true;
        }

        if (productDto.Description != productEntity.Description
            && (productDto.Description is not null && productDto.Description.Trim() != string.Empty
                || productDto.Description is null))
        {
            productEntity.Description = productDto.Description ?? throw new NullReferenceException();
            shouldUpdate = true;
        }

        if (productDto.Price is not null && productDto.Price != productEntity.Price)
        {
            productEntity.Price = productDto.Price.Value;
            shouldUpdate = true;
        }

        if (productDto.ImageUrl != productEntity.ImageUrl
            && (productDto.ImageUrl is not null && productDto.ImageUrl.Trim() != string.Empty
                || productDto.ImageUrl is null))
        {
            productEntity.ImageUrl = productDto.ImageUrl;
            shouldUpdate = true;
        }

        if (productDto.IsFavorite is not null && productDto.IsFavorite != productEntity.IsFavorite)
        {
            productEntity.IsFavorite = productDto.IsFavorite.Value;
            shouldUpdate = true;
        }

        if (productDto.CategoryId is not null
            && productDto.CategoryId != Guid.Empty
            && productDto.CategoryId != productEntity.CategoryId)
        {
            productEntity.CategoryId = productDto.CategoryId.Value;
            shouldUpdate = true;
        }

        if (!shouldUpdate)
            return HttpStatusCode.NotModified;

        try
        {
            if (!await _productRepository.UpdateEntity(productEntity))
                return HttpStatusCode.InternalServerError;
        }
        catch (DbUpdateException)
        {
            return HttpStatusCode.Conflict;
        }

        return HttpStatusCode.OK;
    }
}
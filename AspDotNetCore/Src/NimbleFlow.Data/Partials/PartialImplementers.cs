using NimbleFlow.Data.Partials.Interfaces;

// ReSharper disable once CheckNamespace
namespace NimbleFlow.Data.Models;

public partial class Category : IIdentifiable<Guid>, ICreatedAtDeletedAt
{
}

public partial class Order : IIdentifiable<Guid>, ICreatedAtDeletedAt
{
}

public partial class OrderProduct : ICreatedAtDeletedAt
{
}

public partial class Product : IIdentifiable<Guid>, ICreatedAtDeletedAt
{
}

public partial class Table : IIdentifiable<Guid>, ICreatedAtDeletedAt
{
}
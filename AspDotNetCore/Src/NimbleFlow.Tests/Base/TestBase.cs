using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Controllers;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services;
using NimbleFlow.Data.Context;

namespace NimbleFlow.Tests.Base;

public class TestBase : IDisposable
{
    private readonly SqliteConnection _connection;

    public readonly CategoryController CategoryController;
    public readonly ProductController ProductController;
    public readonly OrderController OrderController;
    public readonly TableController TableController;

    public TestBase()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        _connection.CreateFunction("now", () => DateTime.UtcNow);
        _connection.CreateFunction("gen_random_uuid", Guid.NewGuid);

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<NimbleFlowContext>().UseSqlite(_connection);
        var dbContext = new NimbleFlowContext(dbContextOptionsBuilder.Options);

        var categoryRepository = new CategoryRepository(dbContext);
        var categoryService = new CategoryService(categoryRepository);
        CategoryController = new CategoryController(categoryService);

        var productRepository = new ProductRepository(dbContext);
        var productService = new ProductService(productRepository);
        ProductController = new ProductController(productService);

        var orderRepository = new OrderRepository(dbContext);
        var orderService = new OrderService(orderRepository);
        OrderController = new OrderController(orderService, productService);

        var tableRepository = new TableRepository(dbContext);
        var tableService = new TableService(tableRepository);
        TableController = new TableController(tableService);

        dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}
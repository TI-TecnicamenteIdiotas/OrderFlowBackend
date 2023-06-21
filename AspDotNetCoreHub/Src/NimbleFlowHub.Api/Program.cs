using NimbleFlowHub.Api.Hubs;
using NimbleFlowHub.Api.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapControllers();
app.MapHub<MainHub>("/main");
app.MapGrpcService<TableHubService>();
app.MapGrpcService<CategoryHubService>();

app.Run();
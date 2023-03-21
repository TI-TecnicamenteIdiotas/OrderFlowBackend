using OrderFlow.Api.ConfigurationExtensions;
using OrderFlow.Api.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureStartupOptions();
builder.Services.InjectCors();
builder.Services.InjectDatabases();
builder.Services.InjectRepositories();
builder.Services.InjectServices();
builder.Services.AddControllers();
builder.Services.InjectSwagger(out var enableSwagger);

var app = builder.Build();
if (enableSwagger)
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
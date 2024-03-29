using Hotels.Data;
using Hotels.Data.Database;
using Hotels.Services;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDataLayer(config["ConnectionStrings:SqlConnection"]!);
builder.Services.AddServiceLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();

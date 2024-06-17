using Microsoft.EntityFrameworkCore;
using Loja.Infra.Data;
using Loja.Server.Configurations;
using Loja.Infra.Common.Initializers;
using FluentValidation;

string _routePrefix = "/loja/api";

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureEnvironment();

// Add services to the container.

builder.Services.AddDatabaseConfiguration();

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddDependencyInjection();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.UsePathBase(new PathString(_routePrefix));
app.UseRouting();

try
{
    await using var scope = app.Services.CreateAsyncScope();
    using var db = scope.ServiceProvider.GetService<AppDbContext>();
    await db.Database.MigrateAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred creating migrations. {ex.Message}");
}

app.Run();

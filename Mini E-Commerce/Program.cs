using Microsoft.AspNetCore.Identity;
using Mini_E_Commerce.Extensions;
using Mini_E_Commerce.Infrastructure.Context;
using Mini_E_Commerce.Infrastructure.Extensions;
using Mini_E_Commerce.Infrastructure.SeedDate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCustomJwtAuth(builder.Configuration);
builder.Services.AddSwaggerGenJwtAuth();
builder.Services.AddApplicationServices();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await DataSeeder.SeedDataAsync(applicationDbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    options.EnableTryItOutByDefault());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

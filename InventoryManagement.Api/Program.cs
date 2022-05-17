using InventoryManagement.Api.Data;
using InventoryManagement.Api.Repositories;
using InventoryManagement.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<InventoryManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryManagementConnection"))
);

//Dependency injection
builder.Services.AddScoped<IItemRepository, ItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS 
app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7142", "http://localhost:7142")
    .AllowAnyHeader()
    .WithHeaders(HeaderNames.ContentType)
    .AllowAnyMethod()
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

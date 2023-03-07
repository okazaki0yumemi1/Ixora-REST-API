using Microsoft.EntityFrameworkCore;
using Ixora_REST_API.Persistence;
using Ixora_REST_API.ApiRoutes;
using Ixora_REST_API.Controllers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Ixora_REST_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddScoped<ClientsDbOperations, ClientsDbOperations>();
builder.Services.AddScoped<OrdersDbOperations, OrdersDbOperations>();
builder.Services.AddScoped<OrderDetailsDbOperations, OrderDetailsDbOperations>();
builder.Services.AddScoped<GoodsDbOperations, GoodsDbOperations>();
builder.Services.AddScoped<GoodsTypeDbOperations, GoodsTypeDbOperations>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    TestDataInit.Seed(services);
//}


app.Run();

using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess;
using OrderService.Repositories;
using OrderService.Repository;
using OrderService.Services;
using FluentValidation;
using OrderService.Models.DTO;
using OrderService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderContext>
    (
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDbConnection")
    ));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderServices>();

builder.Services.AddValidatorsFromAssemblyContaining<OrderDTO>();


builder.Services.AddMassTransit(x =>
{
    // Register consumer
    x.AddConsumer<OrderPlacedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Important: connect consumer to RabbitMQ endpoint
        cfg.ReceiveEndpoint("order-service-queue", e =>
        {
            e.ConfigureConsumer<OrderPlacedConsumer>(context);
        });
    });
});

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

app.Run();

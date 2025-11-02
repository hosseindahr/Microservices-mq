using Models.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Services;
using Messaging.Services;
using MassTransit;
using RabbitMQService.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContextPool<OrderDbContext>((serviceProvider, optionsBuilder) =>
{
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Main"));
});
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedCosumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedCosumer>(context);
        });
    });
});

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IMessageBus, MassTransitMessageBus>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using Models.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Services;
using Messaging.Services;
using MassTransit;
using RabbitMQService.Consumer;
using RabbitMQService;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContextPool<OrderDbContext>((serviceProvider, optionsBuilder) =>
{
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Main"));
});
builder.Services.Configure<MessageBrokerSettings>(
    builder.Configuration.GetSection("MessageBroker")
    );
builder.Services.AddSingleton(sp=>
sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value
);
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
        cfg.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });
        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.Durable = true;
            e.AutoDelete = false;
            e.Exclusive = false;
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        });
    });
});
builder.Services.AddMassTransitHostedService();
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

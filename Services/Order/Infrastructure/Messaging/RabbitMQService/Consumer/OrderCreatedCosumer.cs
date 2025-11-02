using MassTransit;
using Models.Entities;
using Models.Services;

namespace RabbitMQService.Consumer
{
    public class OrderCreatedCosumer : IConsumer<Order>
    {
        private readonly IOrderService _service;

        public OrderCreatedCosumer(IOrderService service)
        {
            _service = service;
        }
        public Task Consume(ConsumeContext<Order> context)
        {
            var order = context.Message;
            order.CreateDate = DateTime.Now;
            order.Description = "changed by bus";
            _service.Update(order);
            return Task.CompletedTask;
        }
    }
}

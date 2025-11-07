using MassTransit;
using Messaging.DTO;
using Models.Services;

namespace RabbitMQService.Consumer
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly IOrderService _service;

        public OrderCreatedConsumer(IOrderService service)
        {
            _service = service;
        }
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var orderCreated = context.Message;
            orderCreated.CreateDate = DateTime.Now;
            orderCreated.Description = "changed by bus";

            
           await _service.Update(orderCreated);
            //return await Task.CompletedTask;
        }
    }
}

using MassTransit;
namespace Messaging.Services
{
    public class MassTransitMessageBus : IMessageBus
    {
        private readonly IPublishEndpoint _bus;
        public MassTransitMessageBus(IPublishEndpoint bus)
        {
            _bus = bus;
        }
        public async Task PublishAsync<T>(T message) where T : class
        {
            await _bus.Publish(message);
        }
    }
}

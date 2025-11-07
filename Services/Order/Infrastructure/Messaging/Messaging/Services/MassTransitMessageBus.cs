using MassTransit;
namespace Messaging.Services
{
    public class MassTransitMessageBus : IMessageBus
    {
        private readonly IBus _bus;
        public MassTransitMessageBus(IBus bus)
        {
            _bus = bus;
        }
        public async Task PublishAsync<T>(T message) where T : class
        {
            await _bus.Publish(message);
        }
    }
}

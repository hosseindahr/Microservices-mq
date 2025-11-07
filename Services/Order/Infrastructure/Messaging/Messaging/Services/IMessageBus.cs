namespace Messaging.Services
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(T message) where T : class;
    }
}

using Messaging.DTO;
using Models.Entities;

namespace Models.Services
{
    public interface IOrderService
    {
        Task Add(Order order);
        Task Update(OrderCreated order);
    }
}

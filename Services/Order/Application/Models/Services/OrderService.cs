using Messaging.DTO;
using Messaging.Services;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Data;

namespace Models.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;
        private readonly IMessageBus _bus;

        public OrderService(OrderDbContext context, IMessageBus bus)
        {
            _context = context;
            _bus = bus;
        }
        public async Task Add(Order order)
        {

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            await _bus.PublishAsync(new OrderCreated
            {
                CreateDate=order.CreateDate,
                Description=order.Description,
                Id= order.Id,
                ProductId=order.ProductId,
            });
        }

        public async Task Update(OrderCreated orderCreated)
        {
            Order order = new Order
            {
                Id = orderCreated.Id,
                CreateDate = orderCreated.CreateDate,
                Description = orderCreated.Description,
                ProductId = orderCreated.ProductId
            };
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

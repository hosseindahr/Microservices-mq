using Messaging.Services;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _bus.PublishAsync(order);
        }

        public async Task Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    public interface IOrderService
    {
        Task Add(Order order);
        Task Update(Order order);
    }
}

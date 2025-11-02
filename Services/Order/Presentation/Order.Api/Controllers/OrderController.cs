using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Services;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("AddOrder")]
        public async Task<ActionResult> AddOrder([FromBody] Models.Entities.Order order)
        {
             await _service.Add(order);
            return Ok();
        }
    }
}

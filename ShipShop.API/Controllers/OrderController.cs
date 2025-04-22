using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAll();
            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpGet]
        [Route("[action]/{customerId}")]
        public async Task<IActionResult> GetAllOrdersByCustomerId(int customerId)
        {
            var orders= await _orderService.GetAllOrdersByCustomerId(customerId);
            if (orders == null )
            {
                return NotFound();
            }
            return Ok(orders);
        }
        [HttpPost("{customerId}")]
        public async Task<IActionResult> CreateOrder(OrderCommand orderCommand , int customerId)
        {
            await _orderService.CreateOrder(orderCommand,customerId);
            return Ok();    
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        { 
          
            var order = await _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            await _orderService.Delete(id);
            return Ok($"order with Id {id} was deleted successfully");
        }
    }
}

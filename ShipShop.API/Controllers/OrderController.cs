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
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetAllOrdersCount()
        {
            var orders = await _orderService.GetAll();
            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders.Count);
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
        [HttpGet]
        [Route("sort-by-customer-name/{sortDirection}")]

        public async Task<IActionResult> SortByCustomer(string sortDirection)
        {
            var orders = await _orderService.SortByName(sortDirection);
            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);
        }
 
        [HttpGet]
        [Route("sort-by-id/{sortDirection}")]
        public async Task<IActionResult> SortById(string sortDirection)
        {
            var orders = await _orderService.SortById(sortDirection);
            return Ok(orders);

        }
        [HttpGet]
        [Route("sort-by-creation-date/{sortDirection}")]
        public async Task<IActionResult> SortByCreationDate(string sortDirection)
        {
            var orders = await _orderService.SortByCreation(sortDirection);
            return Ok(orders);
        }

        [HttpGet]
        [Route("sort-by-delivery-date/{sortDirection}")]
        public async Task<IActionResult> SortByDeliveryDate(string sortDirection)
        {
            var orders = await _orderService.SortByDeliveryDate(sortDirection);
            return Ok(orders);
        }
        [HttpPost("{customerId}")]
        public async Task<IActionResult> CreateOrder(OrderCommand orderCommand , int customerId)
        {
            await _orderService.CreateOrder(orderCommand,customerId);
            return Ok();    
        }
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusCommand request)
        {
            if (request.OrderId <= 0 || request.OrderStatusId <= 0)
            {
                return BadRequest("Invalid OrderId or OrderStatusId");
            }

            var result = await _orderService.UpdateOrderStatus(request.OrderId, request.OrderStatusId);

            if (result)
            {
                return Ok();
            }

            return NotFound("Order not found.");
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

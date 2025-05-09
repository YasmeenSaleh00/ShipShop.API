using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessagesController(MessageService messageService)
        {
           _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll ()
        {
            var messages = await _messageService.GetAllMessages();
            if (messages == null || messages.Count == 0)
            {
                return NotFound();
            }
            return Ok(messages);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var message = await _messageService.GetMessages(id);
            if (message == null)
            {
                return BadRequest();
            }
            return Ok(message);
        }
        [HttpPost]

        public async Task<IActionResult> CreateMessage(MessageCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }
            await _messageService.CreateMessage(command);
            return Ok();
        }
    }
}

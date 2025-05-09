using ShipShop.Application.Commands;
using ShipShop.Application.Models;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Services
{
    public class MessageService
    {
        private readonly IMessagesRepository _messagesRepository;

        public MessageService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }
        public async Task CreateMessage(MessageCommand messages)
        {
            if(messages == null) return;
            Messages input = new Messages()
            {
                Name= messages.Name,
                Email= messages.Email,
                Phone= messages.Phone,
                Subject= messages.Subject,
                Message= messages.Message,


            };

            await _messagesRepository.CreateMessage(input);

        }
        public async Task<List<MessageModel>> GetAllMessages()
        {
            var messages = await _messagesRepository.GetAllMessages();  
            List<MessageModel> messageModels= messages.Select(x=>new MessageModel
            {
                Id=x.Id,
                Name=x.Name,
                Email=x.Email,
                Phone=x.Phone,
                Subject=x.Subject,
                Message=x.Message,
                CreatedOn=x.CreatedOn.ToShortDateString(),

            }).ToList();

            return messageModels;

        }
        public async Task<MessageModel> GetMessages(int id)
        {
            var message = await _messagesRepository.GetMessages(id);
            if (message == null)
                return null;
            MessageModel messageModel = new MessageModel()
            {
                Id = id,
                Name = message.Name,
                Email = message.Email,
                Phone = message.Phone,
                Subject = message.Subject,
                Message = message.Message,
                CreatedOn = message.CreatedOn.ToShortDateString(),
            };
            return messageModel;    

        }
    }
}

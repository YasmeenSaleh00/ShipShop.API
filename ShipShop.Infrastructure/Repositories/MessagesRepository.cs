using Microsoft.EntityFrameworkCore;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using ShipShop.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Infrastructure.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly ShipShopDbContext _context;

        public MessagesRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public async Task CreateMessage(Messages messages)
        {
            _context.Messages.Add(messages);
            await _context.SaveChangesAsync();
          }

        public async Task<List<Messages>> GetAllMessages()
        {
            var messages = await _context.Messages.ToListAsync();
            return messages;
        }

        public async Task<Messages> GetMessages(int id)
        {
           var message = await _context.Messages.FirstOrDefaultAsync(x=>x.Id==id);
            return message;
        }
    }
}

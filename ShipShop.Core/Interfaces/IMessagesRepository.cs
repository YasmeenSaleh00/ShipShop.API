using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface IMessagesRepository
    {
        Task CreateMessage(Messages messages);
        Task<Messages> GetMessages(int id);
        Task<List<Messages>> GetAllMessages();
    }
}

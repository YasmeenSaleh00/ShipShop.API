using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll(); 
        Task<User> GetById(int id);
        Task Update(User user);
        Task<List<User>> SortUserByCreateOn (string sortDirection);
        Task DeleteAsync(int id);
        Task CreateNewUser (User user);
   
    }
}

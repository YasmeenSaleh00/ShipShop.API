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
        Task Singup(User user);
        Task Update(User customer);
        Task<User> GetUserByEmail(string email);    
        Task UpdatePassword(User user);
        Task<List<User>> FilterUserByRole(string roleName);
        Task<List<User>> SortUserByCreateOn (string sortDirection);
        Task DeleteAsync(int id);
        Task<User> Login(string username, string password);
    }
}

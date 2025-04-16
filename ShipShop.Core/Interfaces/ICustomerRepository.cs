using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task Singup(Customer user);
        Task Update(Customer customer);
        Task<Customer> GetUserByEmail(string email);
        Task UpdatePassword(Customer user);
        Task DeleteAsync(int id);
        Task<List<Customer>> SortCustomerByCreateOn(string sortDirection);
        Task<List<Customer>> SortCustomerByName(string sortDirection);
        Task<List<Customer>> SortCustomerByEmail(string sortDirection);
        Task<List<Customer>> SortCustomerById(string sortDirection);
    }
}

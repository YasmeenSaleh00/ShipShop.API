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
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task Singup(AddCustomerCommand user)
        {
            if (user == null)
            {
                throw new Exception("You must Add data");
            }

            if (user.Password != user.ConfirmPassword)
            {
                throw new Exception("NOT THE SAME PASSWORD");
            }
            var existingUser = await _customerRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already in use.");
            }

            var newUser = new Customer()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword,
                RoleId = 2,
                CustomerStatusId = 3
            };
            await _customerRepository.Singup(newUser);
        }
        public async Task<List<CustomerModel>> GetAll()
        {
            var users = await _customerRepository.GetAll();
            if (users == null)
            {
                throw new Exception("No Customers ");
            }
            List<CustomerModel> userModels = new List<CustomerModel>();
            userModels = users.Select(x => new CustomerModel
            {
                Id = x.Id,
                CustomerName = x.FullName,
                Email = x.Email,
                CustomerStatus = x.LookupItem.Value,
                Role = x.Role.Name,

                CreateOn = x.CreatedOn.ToShortDateString(),
                UpdateOn = x.UpdatedOn.ToString()

            }).ToList();
            return userModels;

        }
        public async Task<CustomerModel> GetById(int id)
        {
            var user = await _customerRepository.GetById(id);
            if (user == null)
            {
                throw new Exception($"No Customer with the given Id {id}");
            }
       

            CustomerModel model = new CustomerModel()
            {
                Id = id,
                CustomerName = user.FullName,
                Email = user.Email,
                CustomerStatus = user.LookupItem.Value,
                Role = user.Role.Name,

                CreateOn = user.CreatedOn.ToShortDateString(),
                UpdateOn = user.UpdatedOn.ToString()
            };
            return model;
        }
        public async Task UpdatePassword(int id, ChangePasswordCommand command)
        {
            var user = await _customerRepository.GetById(id);
            if (user == null) throw new Exception("User not found");

            user.Password = command.NewPassword;
            user.UpdatedOn = DateTime.Now;




            await _customerRepository.UpdatePassword(user);
        }
        public async Task<List<CustomerModel>> SortCustomerByCreateOn(string sortDirection)
        {
            var user = await _customerRepository.SortCustomerByCreateOn(sortDirection);
            List<CustomerModel> userModels = user.Select(x => new CustomerModel
            {
                Id = x.Id,
                CustomerName = x.FullName,
                Email = x.Email,

                Role = x.Role.Name,
                CustomerStatus=x.LookupItem.Value,
                CreateOn = x.CreatedOn.ToShortDateString(),
                UpdateOn = x.UpdatedOn.ToString()

            }).ToList();
            return userModels;
        }
        public async Task ActivateCustomer(int custId)
        {
            var customer = await _customerRepository.GetById(custId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            customer.CustomerStatusId = 3;

            await _customerRepository.Update(customer);
        }
        public async Task BanCustomer(int custId)
        {
            var customer = await _customerRepository.GetById(custId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            customer.CustomerStatusId = 4;

            await _customerRepository.Update(customer);
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _customerRepository.GetById(id);
            if (user == null)
                throw new Exception("User not found");
            await _customerRepository.DeleteAsync(id);
        }
    }

}

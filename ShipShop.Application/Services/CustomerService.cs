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
       
                CustomerStatusId = 3,
                RoleId=4
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
                FirstName = x.FirstName,
                LastName= x.LastName,
                Email = x.Email,
                CustomerStatus = x.LookupItem.Value,
                CreateOn = x.CreatedOn.ToShortDateString(),
                UpdateOn = x.UpdatedOn.ToString(),
                IsActive=x.IsActive

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
                FirstName = user.FirstName,
                LastName=user.LastName,
                Email = user.Email,
                CustomerStatus = user.LookupItem.Value,
                IsActive=user.IsActive,
                CreateOn = user.CreatedOn.ToShortDateString(),
                UpdateOn = user.UpdatedOn.ToString()
            };
            return model;
        }
        public async Task UpdatePassword( ChangePasswordCommand command)
        {
            var user = await _customerRepository.GetUserByEmail(command.Email);
            if (user == null)
                throw new Exception("User not found");

            if (string.IsNullOrWhiteSpace(command.NewPassword))
                throw new Exception("New password cannot be empty");

            user.Password = command.NewPassword;
            user.ConfirmPassword = command.NewPassword;
            user.UpdatedOn = DateTime.Now;




            await _customerRepository.UpdatePassword(user);
        }
        public async Task UpdateCustomer(int id, UpdateCustomerCommand command)
        {
            var user = await _customerRepository.GetById(id);
            if (user == null) throw new Exception("User not found");

          user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Email = command.Email;
            user.UpdatedOn=DateTime.Now;




            await _customerRepository.Update(user);
        }
        public async Task<List<CustomerModel>> SortCustomerByCreateOn(string sortDirection)
        {
            
            if (string.IsNullOrEmpty(sortDirection) || (sortDirection.ToLower() != "asc" && sortDirection.ToLower() != "desc"))
            {
                throw new ArgumentException("Invalid sort direction. Use 'asc' or 'desc'.", nameof(sortDirection));
            }

           
            var customers = await _customerRepository.SortCustomerByCreateOn(sortDirection);

          
            List<CustomerModel> userModels = customers.Select(x => new CustomerModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                IsActive = x.IsActive,
                CustomerStatus = x.LookupItem.Value,
                CreateOn = x.CreatedOn.ToShortDateString(),
                UpdateOn = x.UpdatedOn.ToString()
            }).ToList();

            return userModels;
        }
        public async Task<List<CustomerModel>> SortCustomerByName(string sortDirection)
        {

            if (string.IsNullOrEmpty(sortDirection) || (sortDirection.ToLower() != "asc" && sortDirection.ToLower() != "desc"))
            {
                throw new ArgumentException("Invalid sort direction. Use 'asc' or 'desc'.", nameof(sortDirection));
            }


            var customers = await _customerRepository.SortCustomerByName(sortDirection);


            List<CustomerModel> userModels = customers.Select(x => new CustomerModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                IsActive = x.IsActive,
                CustomerStatus = x.LookupItem.Value,
                CreateOn = x.CreatedOn.ToShortDateString(),
                UpdateOn = x.UpdatedOn.ToString()
            }).ToList();

            return userModels;

        }
        public async Task<List<CustomerModel>> SortCustomerByEmail(string sortDirection)
        {

            if (string.IsNullOrEmpty(sortDirection) || (sortDirection.ToLower() != "asc" && sortDirection.ToLower() != "desc"))
            {
                throw new ArgumentException("Invalid sort direction. Use 'asc' or 'desc'.", nameof(sortDirection));
            }


            var customers = await _customerRepository.SortCustomerByEmail(sortDirection);


            List<CustomerModel> userModels = customers.Select(x => new CustomerModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                IsActive = x.IsActive,
                CustomerStatus = x.LookupItem.Value,
                CreateOn = x.CreatedOn.ToShortDateString(),
                UpdateOn = x.UpdatedOn.ToString()
            }).ToList();

            return userModels;
        }
        public async Task<List<CustomerModel>> SortCustomerById(string sortDirection)
        {
            if (string.IsNullOrEmpty(sortDirection) || (sortDirection.ToLower() != "asc" && sortDirection.ToLower() != "desc"))
            {
                throw new ArgumentException("Invalid sort direction. Use 'asc' or 'desc'.", nameof(sortDirection));
            }


            var customers = await _customerRepository.SortCustomerById(sortDirection);


            List<CustomerModel> userModels = customers.Select(x => new CustomerModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                IsActive = x.IsActive,
                CustomerStatus = x.LookupItem.Value,
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
            customer.IsActive= true;

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
            customer.IsActive = false;

            await _customerRepository.Update(customer);
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _customerRepository.GetById(id);
            if (user == null)
                throw new Exception("User not found");
            await _customerRepository.DeleteAsync(id);
        }
        public async Task<CustomerModel> GetUserByEmail(string email)
        {
            var user = await _customerRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception($"No Customer with the given email {email}");
            }


            CustomerModel model = new CustomerModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CustomerStatus = user.LookupItem.Value,
                IsActive = user.IsActive,
                CreateOn = user.CreatedOn.ToShortDateString(),
                UpdateOn = user.UpdatedOn.ToString()
            };
            return model;
        }
    }

}

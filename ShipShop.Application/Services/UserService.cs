using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShipShop.Application.Commands;
using ShipShop.Application.Models;
using ShipShop.Application.Queries;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
           
        }
        
        public async Task Singup(AddUserCommand user)
        {
           if (user == null)
            {
                throw new Exception("You must Add data");
            }
          
            if (user.Password != user.ConfirmPassword)
            {
                throw new Exception("NOT THE SAME PASSWORD");
            }
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already in use.");
            }

            var newUser = new User()
           {
               FirstName = user.FirstName,
               LastName = user.LastName,
               Email = user.Email,
               Password = user.Password,
               ConfirmPassword = user.ConfirmPassword,
               RoleId =2 ,
               CustomerStatusId=3
           };
            await _userRepository.Singup(newUser);
         
        

        }
        public async Task<List<UserModel>> GetAll()
        {
            var users = await _userRepository.GetAll();
            if (users == null)
            {
                throw new Exception("No users ");
            }
            List<UserModel> userModels = new List<UserModel>();
            userModels= users.Select(x => new UserModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
            CustomerStatus=x.LookupItem.Value,
                RoleName=x.Role.Name,
              
                CreateOn=x.CreatedOn.ToShortDateString(),
                UpdateOn=x.UpdatedOn.ToString()
                
            }).ToList();
            return userModels;
            
        }
        public async Task<UserModel> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if(user == null)
            {
                throw new Exception($"No user with the given Id {id}");
            }
            if (user.Role == null)
            {
                throw new Exception($"No role found for user with Id {id}");
            }

            UserModel model = new UserModel()
            {
                Id = id,
                FullName = user.FullName,
                Email = user.Email,
                CustomerStatus = user.LookupItem.Value,
                RoleName = user.Role.Name,
         
                CreateOn = user.CreatedOn.ToShortDateString(),
                UpdateOn = user.UpdatedOn.ToString()
            };
            return model;   
        }
        public async Task UpdatePassword( int id , ChangePasswordCommand command)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) throw new Exception("User not found");

            user.Password = command.NewPassword;
                user.UpdatedOn = DateTime.Now;
           
          
       

            await _userRepository.UpdatePassword(user);
        }
        public async Task<List<UserModel>> FilterUserByRole(string roleName)
        {
           
            if(roleName == null)
            {
                throw new Exception("Role name cant be Null");
            }
            var users = await _userRepository.FilterUserByRole(roleName);
       
            var model = users.Select(x => new UserModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
       
                RoleName = x.Role.Name,
           
                CreateOn = x.CreatedOn.ToShortDateString(),
                UpdateOn=x.UpdatedOn.ToString(),
                
                
            }).ToList();

            return model;


        }
        public async Task<List<UserModel>> SortUserByCreateOn(string sortDirection)
        {
              var user = await _userRepository.SortUserByCreateOn(sortDirection);
            List<UserModel> userModels = user.Select(x => new UserModel
            {
                Id=x.Id,
                FullName = x.FullName,
                Email = x.Email,
            
                RoleName=x.Role.Name   ,
      
                CreateOn = x.CreatedOn.ToShortDateString(),
                UpdateOn=x.UpdatedOn.ToString()

            }).ToList();
            return userModels;
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new Exception("User not found");
            await _userRepository.DeleteAsync(id);
        }
        public async Task ActivateCustomer(int custId)
        {
            var customer = await _userRepository.GetById(custId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            customer.CustomerStatusId = 3;

            await _userRepository.Update(customer);
        }
        public async Task BanCustomer(int custId)
        {
            var customer = await _userRepository.GetById(custId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            customer.CustomerStatusId = 4;

            await _userRepository.Update(customer);
        }
    }
}

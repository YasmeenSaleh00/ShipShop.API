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




        public async Task CreateNewUser(AddUserCommand user)
        {
            var person = new User()
            {
                FirstName = user.FirstName,
                LastName= user.LastName,    
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword,
                RoleId = user.RoleId,
                
               
            };
            await _userRepository.CreateNewUser(person);

        }


        public async Task<List<UserModel>> GetAll()
        {
            var users = await _userRepository.GetAll();
            if (users == null)
            {
                throw new Exception("No users ");
            }
            List<UserModel> userModels = new List<UserModel>();
            userModels = users.Select(x => new UserModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName=x.LastName,
                Email = x.Email,

                RoleName = x.Role.Name,
                IsActive= x.IsActive,   

                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdateOn = x.UpdatedOn.ToString()

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
                FirstName = user.FirstName,
                LastName=user.LastName, 
                Email = user.Email,
                IsActive = user.IsActive,
                RoleName = user.Role.Name,

                CreatedOn = user.CreatedOn.ToShortDateString(),
                UpdateOn = user.UpdatedOn.ToString()
            };
            return model;   
        }
 
    
        public async Task<List<UserModel>> SortUserByCreateOn(string sortDirection)
        {
              var user = await _userRepository.SortUserByCreateOn(sortDirection);
            List<UserModel> userModels = user.Select(x => new UserModel
            {
                Id=x.Id,
                FirstName = x.FirstName,
                LastName=x.LastName,
                Email = x.Email,
            
                RoleName=x.Role.Name,   
                IsActive = x.IsActive,

                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdateOn=x.UpdatedOn.ToString(),
               

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
        public async Task UpdateUserPassword(int id , UpdatePasswordCommand command)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) throw new Exception("User not found");

            user.Password = command.NewPassword;
            user.ConfirmPassword = command.ConfirmPassword;
            user.UpdatedOn = DateTime.Now;
            _userRepository.Update(user);   
        }
        
    }
}

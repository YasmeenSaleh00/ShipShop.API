using ShipShop.Application.Commands;
using ShipShop.Application.Models;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Services
{
    public class RoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<int> Add(RoleCommand command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var role = new Role()
            {
                Name = command.Name,
            };  
            var id = await _roleRepository.Add(role);
            return id;  
              
        }
        public async Task Delete(int id)
        {
            var role = await _roleRepository.GetById(id);
            if (role == null)
                throw new Exception("No Role with the Given Id");
            await _roleRepository.Delete(id);
        }
        public async Task<List<RoleModel>> GetAll()
        {
         var model = await _roleRepository.GetAll();
            List<RoleModel> roleModels = new List<RoleModel>();
            roleModels = model.Select(x => new RoleModel
            {
                Id = x.Id,
                Name = x.Name,
                CreatedOn=x.CreatedOn.ToShortDateString(),
                UpdatedOn=x.UpdatedOn.ToString(),
                IsActive = x.IsActive

            }).ToList();    
            return roleModels;
        }
        public async Task<RoleModel> GetById(int id)
        {
            var model = await _roleRepository.GetById(id);
            RoleModel role = new RoleModel();

            role.Id = model.Id;
            role.Name = model.Name;
            role.IsActive=model.IsActive;
            role.CreatedOn=model.CreatedOn.ToShortDateString();
            role.UpdatedOn = model.UpdatedOn.ToString();

            return role;

           
        }
        public async Task<List<RoleModel>> SortCRoleByCreatedOn(string sortDirection)
        {

            if (string.IsNullOrEmpty(sortDirection) || (sortDirection.ToLower() != "asc" && sortDirection.ToLower() != "desc"))
            {
                throw new ArgumentException("Invalid sort direction. Use 'asc' or 'desc'.", nameof(sortDirection));
            }


            var customers = await _roleRepository.SortByCreationDate(sortDirection);


            List<RoleModel> userModels = customers.Select(x => new RoleModel
            {
                Id = x.Id,
               Name = x.Name,
                IsActive = x.IsActive,
                CreatedOn = x.CreatedOn.ToShortDateString(),
            
            }).ToList();

            return userModels;

        }

        public async Task<List<RoleModel>> SortCRoleById(string sortDirection)
        {

            if (string.IsNullOrEmpty(sortDirection) || (sortDirection.ToLower() != "asc" && sortDirection.ToLower() != "desc"))
            {
                throw new ArgumentException("Invalid sort direction. Use 'asc' or 'desc'.", nameof(sortDirection));
            }


            var customers = await _roleRepository.SortById(sortDirection);


            List<RoleModel> userModels = customers.Select(x => new RoleModel
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                CreatedOn = x.CreatedOn.ToShortDateString(),

            }).ToList();

            return userModels;

        }

        public async Task<List<RoleModel>> SortCRoleByName(string sortDirection)
        {

            if (string.IsNullOrEmpty(sortDirection) || (sortDirection.ToLower() != "asc" && sortDirection.ToLower() != "desc"))
            {
                throw new ArgumentException("Invalid sort direction. Use 'asc' or 'desc'.", nameof(sortDirection));
            }


            var customers = await _roleRepository.SortByName(sortDirection);


            List<RoleModel> userModels = customers.Select(x => new RoleModel
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                CreatedOn = x.CreatedOn.ToShortDateString(),

            }).ToList();

            return userModels;

        }
        public async Task Update(RoleCommand input,int id)
        {
            var role = new Role
            {
                Id=id,
                Name=input.Name,
                UpdatedOn = DateTime.Now,
            };
            await _roleRepository.Update(role);
        }
    

    }
}

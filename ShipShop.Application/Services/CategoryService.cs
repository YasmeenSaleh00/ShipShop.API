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
    public class CategoryService
    {
        private readonly IRepository<Category> _repository;
      

        public CategoryService(IRepository<Category> repository)
        {
            this._repository = repository;
          
        }
        public async Task<int> AddCategory(CategoryCommand command)
        {
            if(command == null)
            {
                throw new Exception("Please Fill in the data");
            }
            var category=new Category()
            {
                Name = command.Name,
                NameAr=command.NameAr,
                DescriptionAr=command.DescriptionAr,
              
                Description = command.Description,
              
            };
            return await _repository.Add(category);
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            var categories = await _repository.GetAll();

            List<CategoryModel> result = new List<CategoryModel>();

            result = categories.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
                NameAr = x.NameAr,
                DescriptionAr=x.DescriptionAr,


                IsActive = x.IsActive
            }).ToList();

            return result;

        }
        public async Task<CategoryModel> GetById(int id)
        {
            var category = await _repository.GetById(id);
            if (category == null)
                return null;
            CategoryModel result = new CategoryModel();
            result.Id = category.Id;
            result.Name = category.Name;
            result.Description = category.Description;
            result.NameAr = category.NameAr;
            result.DescriptionAr = category.DescriptionAr;
            result.CreatedOn = category.CreatedOn.ToShortDateString();
            result.UpdatedOn = category.UpdatedOn.ToString()   ;
            result.IsActive = category.IsActive;    
            return result;
        }

        public async Task UpdateCategory(int id, UpdateCategoryCommand command)
        {
     
            var category = new Category()
            {
                Id=id,
                Name = command.Name,
                Description = command.Description,
           
               DescriptionAr=command.DescriptionAr,
               NameAr=command.NameAr,
               IsActive=command.IsActive,
        
                UpdatedOn = DateTime.Now,

            };

            await _repository.Update(category);
        }
        public async Task DeleteCategory(int id)
        {
            var category = await _repository.GetById(id);
            if(category == null)
            {
                throw new Exception("no category with the given id");
            }
            await _repository.Delete(id);
        }
    }
}

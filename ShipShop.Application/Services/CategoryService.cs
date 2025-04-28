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
            _repository = repository;
        }

        public async Task<int> AddCategory(CategoryCommand command)
        {
            if (command == null)
            {
                throw new Exception("Please Fill in the data");
            }
            var category = new Category()
            {
                Name = command.Name,
                NameAr = command.NameAr,
                DescriptionAr = command.DescriptionAr,
                ImageUrl = command.ImageUrl,

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
                DescriptionAr = x.DescriptionAr,
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",


                IsActive = x.IsActive
            }).ToList();

            return result;

        }
        public async Task<List<CategoryModel>> SortByCreationDate(string sortDirection)
        {
            var category = await _repository.SortByCreationDate(sortDirection);
            List<CategoryModel> productModels = category.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",

                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),

                IsActive = x.IsActive,

            }).ToList();
            return productModels;

        }
        public async Task<List<CategoryModel>> SortByName(string sortDirection)
        {
            var category = await _repository.SortByName(sortDirection);
            List<CategoryModel> productModels = category.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",

                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),

                IsActive = x.IsActive,

            }).ToList();
            return productModels;

        }

        public async Task<List<CategoryModel>> SortById(string sortDirection)
        {
            var category = await _repository.SortById(sortDirection);
            List<CategoryModel> productModels = category.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",

                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),

                IsActive = x.IsActive,

            }).ToList();
            return productModels;

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
            result.UpdatedOn = category.UpdatedOn.ToString();
            result.IsActive = category.IsActive;
            result.ImageUrl = $"https://localhost:7057/Images/{category.ImageUrl}";
            return result;
        }

        public async Task UpdateCategory(int id, UpdateCategoryCommand command)
        {
            var category = await _repository.GetById(id);
            if (category == null)
            {
                throw new Exception("Category not found.");
            }

           
            if (!string.IsNullOrEmpty(command.ImageUrl) && category.ImageUrl != command.ImageUrl)
            {
                category.ImageUrl = command.ImageUrl;
            }

            category.Name = command.Name;
            category.Description = command.Description;
            category.DescriptionAr = command.DescriptionAr;
            category.NameAr = command.NameAr;
            category.IsActive = command.IsActive;
            category.UpdatedOn = DateTime.Now;

            await _repository.Update(category);
        }


        public async Task DeleteCategory(int id)
        {
            var category = await _repository.GetById(id);
            if (category == null)
            {
                throw new Exception("no category with the given id");
            }
            await _repository.Delete(id);
        }
    }
}

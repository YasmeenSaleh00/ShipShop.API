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
    public class SubCategoryService
    {
        private readonly ISubCategoryRepository _repository;
      

        public SubCategoryService(ISubCategoryRepository repository)
        {
            this._repository = repository;
          
        }
        public async Task<int> AddCategory(SubCategoryCommand command)
        {
            if(command == null)
            {
                throw new Exception("Please Fill in the data");
            }
            var category=new SubCategory()
            {
                Name = command.Name,
                NameAr=command.NameAr,
                DescriptionAr=command.DescriptionAr,
                ImageUrl= command.ImageUrl, 
               CategoryId=command.CategoryId,
                Description = command.Description,
              
            };
            return await _repository.Add(category);
        }

        public async Task<List<SubCategoryModel>> GetAllCategory()
        {
            var categories = await _repository.GetAll();

            List<SubCategoryModel> result = new List<SubCategoryModel>();

            result = categories.Select(x => new SubCategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
                NameAr = x.NameAr,
                DescriptionAr=x.DescriptionAr,
                ImageUrl = x.ImageUrl,
                MainCategory=x.Category.Name,


                IsActive = x.IsActive
            }).ToList();

            return result;

        }
        public async Task<List<SubCategoryModel>> SortByCreationDate(string sortDirection)
        {
            var category = await _repository.SortByCreationDate(sortDirection);
            List<SubCategoryModel> categoryModels = category.Select(x => new SubCategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                ImageUrl = x.ImageUrl,
                MainCategory = x.Category.Name,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
              
                IsActive = x.IsActive,
            
            }).ToList();
            return categoryModels;

        }
        public async Task<List<SubCategoryModel>> SortByName(string sortDirection)
        {
            var category = await _repository.SortByName(sortDirection);
            List<SubCategoryModel> categoryModels = category.Select(x => new SubCategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                DescriptionAr = x.DescriptionAr,
                MainCategory = x.Category.Name,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),

                IsActive = x.IsActive,

            }).ToList();
            return categoryModels;

        }
        public async Task<List<SubCategoryModel>> SortById(string sortDirection)
        {
            var category = await _repository.SortById(sortDirection);
            List<SubCategoryModel> categoryModels = category.Select(x => new SubCategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                ImageUrl = x.ImageUrl,
                MainCategory = x.Category.Name,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),

                IsActive = x.IsActive,

            }).ToList();
            return categoryModels;

        }
        public async Task<SubCategoryModel> GetById(int id)
        {
            var category = await _repository.GetById(id);
            if (category == null)
                return null;
            SubCategoryModel result = new SubCategoryModel();
            result.Id = category.Id;
            result.Name = category.Name;
            result.Description = category.Description;
            result.NameAr = category.NameAr;
            result.DescriptionAr = category.DescriptionAr;
            result.CreatedOn = category.CreatedOn.ToShortDateString();
            result.UpdatedOn = category.UpdatedOn.ToString()   ;
            result.IsActive = category.IsActive;
            result.ImageUrl = category.ImageUrl;
            result.MainCategory= category.Category.Name; 
            return result;
        }
        public async Task<List<SubCategoryModel>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var category =await _repository.GetSubCategoriesByCategoryId(categoryId);
            List<SubCategoryModel> categoryModels = category.Select(x => new SubCategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                ImageUrl = x.ImageUrl,
                MainCategory = x.Category.Name,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),

                IsActive = x.IsActive,

            }).ToList();

            return categoryModels;

        }
        public async Task UpdateCategory(int id, UpdateSubCategoryCommand command)
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
            if(category == null)
            {
                throw new Exception("no category with the given id");
            }
            await _repository.Delete(id);
        }
    }
}

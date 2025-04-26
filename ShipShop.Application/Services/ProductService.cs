using ShipShop.Application.Commands;
using ShipShop.Application.Models;
using ShipShop.Application.Queries;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> AddProduct(ProductCommand command)
        {
            Product product = new Product()
            {
                CategoryId = command.CategoryId,
                Name = command.Name,
                NameAr=command.NameAr,  
                CreatedOn = DateTime.Now,
                BrandId = command.BrandId,
                 Quantity=command.Quantity,

                Description = command.Description,
                DescriptionAr= command.DescriptionAr,
               
                Price = command.Price,
                ProductStatusId = 1,
                TaxPercentage = command.TaxPercentage,
                ImageUrl = command.ImageUrl,

            };

            return await _productRepository.Add(product);
        }
        public async Task UpdateProduct(UpdateProductCommand command, int id)
        {
            Product product = new Product()
            {
                Id = id,
                Name = command.Name,
                CategoryId = command.CategoryId,
                NameAr = command.NameAr,
                Description = command.Description,
                DescriptionAr = command.DescriptionAr,
                Price = command.Price,
               TaxPercentage= command.TaxPercentage,
               BrandId = command.BrandId,
                Quantity = command.Quantity,
                ProductStatusId = command.ProductStatusId,

                UpdatedOn = DateTime.Now
            };

            await _productRepository.Update(product);
        }
     
   
        public async Task Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if(product == null)
            {
                throw new Exception("No product with the given Id");
            }
            await _productRepository.Delete(id);
        }
        public async Task<List<ProductModel>> GetAll()
        {
         var products = await _productRepository.GetAll();
            List<ProductModel> result = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr= x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                Price = x.Price,
                TaxPercentage= x.TaxPercentage,
                BrandName=x.Brand.Name,
                ProductStatus = x.LookupItem.Value,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
                CategoryName = x.Category.Name,
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",
                IsActive= x.IsActive,  
                Quantity=x.Quantity

            }).ToList();

            return result;

        }
        public async Task<ProductModel>  GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return null;
            ProductModel productmodel = new ProductModel();

            productmodel.Id = product.Id;
            productmodel.Name = product.Name;
            productmodel.NameAr = product.NameAr;
            productmodel.Description = product.Description;
            productmodel.DescriptionAr = product.DescriptionAr;
            productmodel.Price = product.Price;
            productmodel.TaxPercentage = product.TaxPercentage;
            productmodel.CategoryName = product.Category.Name;
            productmodel.BrandName = product.Brand.NameAr;
            productmodel.ProductStatus = product.LookupItem.Value;
            productmodel.CreatedOn = product.CreatedOn.ToShortDateString();
            productmodel.ImageUrl = $"https://localhost:7057/Images/{product.ImageUrl}";
            productmodel.UpdatedOn = product.UpdatedOn.ToString();
            productmodel.Quantity= product.Quantity;
            productmodel.IsActive=product.IsActive; 

            return productmodel;


        }
        public async Task<List<ProductModel>> GetProductsByFilters(string categoryName)
        {
            var products = await _productRepository.GetProductsByFilters( categoryName);
            List<ProductModel> productModels = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                Price = x.Price,
                TaxPercentage = x.TaxPercentage,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",
                IsActive = x.IsActive,
                CategoryName = x.Category.Name,
                BrandName = x.Brand.Name,
                ProductStatus = x.LookupItem.Value,
                Quantity = x.Quantity,
            }).ToList();
            return productModels;

        }

        public async Task<List<ProductModel>> GetProductsBrand(string brandName)
        {
            var products = await _productRepository.GetProductsByBrand(brandName);
            List<ProductModel> productModels = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                Price = x.Price,
                TaxPercentage = x.TaxPercentage,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",
                IsActive = x.IsActive,
                CategoryName = x.Category.Name,
                BrandName = x.Brand.Name,
                ProductStatus = x.LookupItem.Value,
                Quantity = x.Quantity,
            }).ToList();
            return productModels;

        }
        public async Task<List<ProductModel>> SortByName(string sortDirection)
        {
            var products = await _productRepository.SortByName( sortDirection);
            List<ProductModel> productModels = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                Price = x.Price,
                TaxPercentage = x.TaxPercentage,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",
                IsActive = x.IsActive,  
                CategoryName = x.Category.Name, 
               BrandName=x.Brand.Name,
               ProductStatus=x.LookupItem.Value,
               Quantity=x.Quantity, 
            }).ToList();
            return productModels;

        }

        public async Task<List<ProductModel>> SortByCreationDate(string sortDirection)
        {
            var products = await _productRepository.SortByCreationDate(sortDirection);
            List<ProductModel> productModels = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                Price = x.Price,
                TaxPercentage = x.TaxPercentage,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",
                IsActive= x.IsActive,
                CategoryName = x.Category.Name,
                BrandName = x.Brand.Name,
                ProductStatus = x.LookupItem.Value,
                Quantity = x.Quantity,
            }).ToList();
            return productModels;

        }
        public async Task<List<ProductModel>> SortByPrice(string sortDirection)
        {
            var products = await _productRepository.SortByPrice(sortDirection);
            List<ProductModel> productModels = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                Price = x.Price,
                TaxPercentage = x.TaxPercentage,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",
                IsActive = x.IsActive,
                CategoryName = x.Category.Name,
                BrandName = x.Brand.Name,
                ProductStatus = x.LookupItem.Value,
                Quantity = x.Quantity,
            }).ToList();
            return productModels;
        }
        public async Task<List<ProductModel>> SortById(string sortDirection)
        {
            var products = await _productRepository.SortById(sortDirection);
            List<ProductModel> productModels = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                Price = x.Price,
                TaxPercentage = x.TaxPercentage,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString(),
                ImageUrl = $"https://localhost:7057/Images/{x.ImageUrl}",
                IsActive= x.IsActive,
                CategoryName = x.Category.Name,
                BrandName = x.Brand.Name,
                ProductStatus = x.LookupItem.Value,
                Quantity = x.Quantity,
            }).ToList();
            return productModels;
        }

    }
}

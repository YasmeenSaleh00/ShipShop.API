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
    public class BrandService
    {
        private readonly IRepository<Brand> _brandRepository;

        public BrandService(IRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<int> AddNewBrand(BrandCommand command)
        {
            var brand = new Brand()
            {
                Name = command.Name,
                NameAr = command.NameAr,
                ImagePath = command.ImagePath,
                CreatedOn=DateTime.Now,
            };
            return await _brandRepository.Add(brand);
        }
        public async Task<List<BrandModel>> GetAllBrand()
        {
            var brands = await _brandRepository.GetAll();

            List<BrandModel> result = new List<BrandModel>();

            result = brands.Select(x => new BrandModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr=x.NameAr,
                ImagePath = x.ImagePath,
                CreatedOn = x.CreatedOn.ToShortDateString(),
                UpdatedOn = x.UpdatedOn.ToString()
            }).ToList();

            return result;
        }
        public async Task<BrandModel> GetById(int id)
        {
            var brand = await _brandRepository.GetById(id);
            if (brand == null)
                return null;
            BrandModel result = new BrandModel();
            result.Id = brand.Id;
            result.Name = brand.Name;
            result.NameAr= brand.NameAr;
            result.ImagePath = brand.ImagePath;
            result.CreatedOn = brand.CreatedOn.ToShortDateString();
          
            result.UpdatedOn = brand.UpdatedOn.ToString();
            return result;


        }
        public async Task UpdateBrand(int id, BrandCommand command)
        {
            var brand = await _brandRepository.GetById(id);
            if(brand == null)
            {
                throw new Exception("No Brand with the Given Id");
            }
            var result = new Brand()
            {
                Id = id,
                Name = command.Name,
                NameAr= command.NameAr,
                ImagePath = command.ImagePath,
                UpdatedOn = DateTime.Now

            };
            await _brandRepository.Update(result);

        }
        public async Task DeleteBrand (int id)
        {
            var brand = await _brandRepository.GetById(id);
            if (brand == null)
            {
                throw new Exception("No Brand with the Given Id");
            }
            await _brandRepository.Delete(id);

        }
    }

}

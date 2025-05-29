using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<List<Product>> GetProductsByBrand(string brandName);
        Task<List<Product>> GetProductsByFilters(string categoryName);
        Task<List<Product>> SortByPrice(string sortDirection);
        Task<List<Product>> Search(string name);


    }
}

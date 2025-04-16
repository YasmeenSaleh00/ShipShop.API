using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface IRepository<T> where T : MainEntity
    {
        Task<List<T>> GetAll();
      Task<T> GetById(int id);
       Task<int> Add(T input);
        Task Update(T input);
        Task Delete(int id);
        Task<List<T>> SortByName(string sortDirection);
   
        Task<List<T>> SortById(string sortDirection);
        Task<List<T>> SortByCreationDate(string sortDirection);

    }
}

using Microsoft.EntityFrameworkCore;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using ShipShop.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Infrastructure.Repositories
{
    public class LookupRepository : ILookupRepository
    {
        private readonly ShipShopDbContext _dbContext;

        public LookupRepository(ShipShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LookupItem> GetLookupItemById(int id)
        {
           var lookupitem =await _dbContext.LookupItems.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
            return lookupitem;
        }

        public async Task<List<LookupItem>> GetLookupItemValueByType(int LookupTypeId)
        {
            var lookups = await _dbContext.LookupItems.Include(x=>x.LookupType).AsNoTracking().ToListAsync();  
            return lookups;
        }

        


    }
}

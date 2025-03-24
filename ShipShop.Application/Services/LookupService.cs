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
    public class LookupService
    {
        private readonly ILookupRepository _lookupRepository;

        public LookupService(ILookupRepository lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }
        public async Task<LookupModel> GetLookupItemById(int id)
        {
            if(id == 0 || id<0)
            {
                throw new Exception("Not Valid Id");

            }
            var lookups = await _lookupRepository.GetLookupItemById(id);
            if (lookups == null)
            {
                return null;

            }
            LookupModel lookupModel = new LookupModel();
            lookupModel.Id = id;
            lookupModel.Value = lookups.Value;
            lookupModel.CreatedOn=lookups.CreatedOn.ToShortDateString();    
            lookupModel.IsActive=lookups.IsActive;
       
            return lookupModel;
        }
     
        public async Task<List<LookupTypeModel>> GetLookupItemValueByType(int LookupTypeId)
        {
            var lookups = await _lookupRepository.GetLookupItemValueByType(LookupTypeId);
            List<LookupTypeModel> models = new List<LookupTypeModel>();
            models = lookups.Select(s => new LookupTypeModel
            {
                Id = s.Id,
                TypeName = s.LookupType.Name,
                Value = s.Value,

                

            }).ToList();    
            return models;

        }
      

    }
}

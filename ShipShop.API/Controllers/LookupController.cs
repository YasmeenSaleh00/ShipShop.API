using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly LookupService _lookupService;

        public LookupController(LookupService lookupService)
        {
            _lookupService = lookupService;
        }
        [HttpGet]
        [Route("[action]/{LookupTypeId}")]
        public async Task<IActionResult> GetLookupItemValueByType(int LookupTypeId)
        {
            var lookup = await _lookupService.GetLookupItemValueByType(LookupTypeId);
            if (lookup == null || lookup.Count ==0)
            {
                return NotFound();  
            }
            return Ok(lookup);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLookupItemById(int id)
        {
            var lookup= await _lookupService.GetLookupItemById(id);
            if(lookup == null)
            {
                return NotFound();
            }
            return Ok(lookup);
        }
      
        
    }
}

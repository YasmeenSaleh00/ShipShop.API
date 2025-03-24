using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAll();
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);

        }
    
        [HttpPost]
        public async Task<IActionResult> AddNewRole (RoleCommand command)
        {
          var id=  await _roleService.Add(command);
            return Ok($"New Role with Id {id} Successfully Added ");
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(RoleCommand command , int id)
        {
          
            await _roleService.Update(command, id);
            return Ok($"Role with Id {id} was updated successfully");

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole (int id)
        {
            if(id == 0 || id < 0)
            {
                return BadRequest();    
            }
          await  _roleService.Delete(id);
            return Ok("Role  Successfully Deleted");
        }
   
    }
}

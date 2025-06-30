using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Add,Admin")]
        public async Task<IActionResult> AddNewRole (RoleCommand command)
        {
          var id=  await _roleService.Add(command);
            return Ok();
        }
        [HttpGet("sort-by-creation date/{sortDirection}")]
        public async Task<IActionResult> SortRoleByCreateOn(string sortDirection)
        {
            var users = await _roleService.SortCRoleByCreatedOn(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet("sort-by-Name/{sortDirection}")]
        public async Task<IActionResult> SortRoleByName(string sortDirection)
        {
            var users = await _roleService.SortCRoleByName(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
      

        [HttpGet("sort-by-Id/{sortDirection}")]
        public async Task<IActionResult> SortRoleById(string sortDirection)
        {
            var users = await _roleService.SortCRoleById(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Edit,Admin")]
        public async Task<IActionResult> UpdateRole(RoleCommand command , int id)
        {
          
            await _roleService.Update(command, id);
            return Ok();

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Delete,Admin")]
        public async Task<IActionResult> DeleteRole (int id)
        {
            if(id == 0 || id < 0)
            {
                return BadRequest();    
            }
          await  _roleService.Delete(id);
            return Ok();
        }
   
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Models;
using ShipShop.Application.Queries;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// This EndPoint To Show All Users
        /// </summary>
        [HttpGet]   
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAll();
            if(users == null || users.Count == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }
        /// <summary>
        /// This EndPoint To get User info
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if(id == 0 || id <0)
            {
                return BadRequest();
            }
            var user = await _userService.GetById(id);
            return Ok(user);
        }
    
        /// <summary>
        /// This EndPoint To get User sorted  Depend on creation date
        /// </summary>
        [HttpGet("sort-by-creation date/{sortDirection}")]
        public async Task<IActionResult> SortUserByCreateOn(string sortDirection)
        {
            var users = await _userService.SortUserByCreateOn(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
     
        

      
       
        /// <summary>
        /// This EndPoint to Delete User
        /// </summary>
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok($"User with the id {id} deleted Successfully ");
        }
      
    }
}

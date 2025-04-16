using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Models;
using ShipShop.Application.Queries;
using ShipShop.Application.Services;
using ShipShop.Core.Entities;

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

            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
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

        [HttpGet("sort-by-creation date/{sortDirection}")]
        public async Task<IActionResult> SortUserByCreateOn(string sortDirection)
        {
            var users = await _userService.SortUserByCreation(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet("sort-by-Name/{sortDirection}")]
        public async Task<IActionResult> SortUserByName(string sortDirection)
        {
            var users = await _userService.SortUserByName(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet("sort-by-Email/{sortDirection}")]
        public async Task<IActionResult> SortUserByEmail(string sortDirection)
        {
            var users = await _userService.SortUserByEmail(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("sort-by-Id/{sortDirection}")]
        public async Task<IActionResult> SortCustomerById(string sortDirection)
        {
            var users = await _userService.SortUserById(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        /// <summary>
        /// This EndPoint to Add new  User
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateNewUser(AddUserCommand command)
        {
           await _userService.CreateNewUser(command);
            return Ok();

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePassword(int id, UpdatePasswordCommand command)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.UpdateUserPassword(id, command);
            return Ok();
        }
        /// <summary>
        /// This EndPoint to Delete User
        /// </summary>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }
      
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;
using ShipShop.Core.Interfaces;
using ShipShop.Infrastructure.Repositories;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }
    
        /// <summary>
        /// This EndPoint To Show All Customers
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAll();
            if (customers == null || customers.Count == 0)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetAllCustomersCount()
        {
            var customers = await _customerService.GetAll();
            if (customers == null || customers.Count == 0)
            {
                return NotFound();
            }
            return Ok(customers.Count);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest();
            }
            var user = await _customerService.GetById(id);
            return Ok(user);
        }
        /// <summary>
        /// This EndPoint To get customer sorted  Depend on creation date
        /// </summary>
        [HttpGet("sort-by-creation date/{sortDirection}")]
        public async Task<IActionResult> SortUserByCreateOn(string sortDirection)
        {
            var users = await _customerService.SortCustomerByCreateOn(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet("sort-by-Name/{sortDirection}")]
        public async Task<IActionResult> SortUserByName(string sortDirection)
        {
            var users = await _customerService.SortCustomerByName(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet("sort-by-Email/{sortDirection}")]
        public async Task<IActionResult> SortUserByEmail(string sortDirection)
        {
            var users = await _customerService.SortCustomerByEmail(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("sort-by-Id/{sortDirection}")]
        public async Task<IActionResult> SortCustomerById(string sortDirection)
        {
            var users = await _customerService.SortCustomerById(sortDirection);
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
    
        [HttpPost]
     
        public async Task<IActionResult> Registertion(AddCustomerCommand command)
        {
            await _customerService.Singup(command);
            return Ok();
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerCommand command)
        {
              await _customerService.UpdateCustomer(id, command);
            return Ok();

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _customerService.DeleteAsync(id); 
            return Ok();
        }
        [HttpPut]
        [Route("[action]/{custId}")]
        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> BanCustomer(int custId)
        {
            await _customerService.BanCustomer(custId);
            return Ok();
        }
        [HttpPut]
        [Route("[action]/{custId}")]
        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> ActivateCustomer(int custId)
        {
            await _customerService.ActivateCustomer(custId);    
            return Ok();
        }
    }
}
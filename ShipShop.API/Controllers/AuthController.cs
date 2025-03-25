using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Models;
using ShipShop.Application.Queries;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// This Endpoint For login 
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationQuery model)
        {
            AuthenticationModel result = await _authService.Login(model);

            if (result == null)
            {
                return BadRequest("Email Or Password is incorrect");
            }
            else
            {
                return Ok(result);
            }
        }
    }
}

﻿using Microsoft.AspNetCore.Http;
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
     
        public async Task<IActionResult> Login([FromBody] AuthenticationQuery model)
        {
            AuthenticationModel result = await _authService.Login(model);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return BadRequest(new { message = result.ErrorMessage });
            }
            else
            {
                return Ok(result);
            }
        }
    }
}

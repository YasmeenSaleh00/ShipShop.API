﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShipShop.Application.Models;
using ShipShop.Application.Queries;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration configuration;
    

        public AuthService(IAuthRepository authRepository,IConfiguration configuration)
        {
            _authRepository = authRepository;
            this.configuration = configuration;
        
        }
        public async Task<AuthenticationModel> Login(AuthenticationQuery query)
        {
            try
            {
                var user = await _authRepository.Login(query.Email, query.Password);

                if (user == null)
                {
                    return new AuthenticationModel
                    {
                        AccessToken = string.Empty,
                        ExpiresAt = DateTime.UtcNow,
                        ErrorMessage = "Invalid email or password."
                    };
                }

                int CartId = 0;
                int WishlistId = 0;

                if (user is Customer customer)
                {
                    if (customer.Carts != null && customer.Carts.Any())
                        CartId = customer.Carts.First().Id;

                    if (customer.WishList != null && customer.WishList.Any())
                        WishlistId = customer.WishList.First().Id;
                }

                AuthenticationModel authenticationModel = new AuthenticationModel
                {
                    AccessToken = GenerateToken(user.Id, user.Role.Name, CartId, WishlistId),
                    ExpiresAt = DateTime.UtcNow.AddHours(2),
                    ErrorMessage = string.Empty
                };

                return authenticationModel;
            }
            catch (Exception ex)
            {
                return new AuthenticationModel
                {
                    AccessToken = string.Empty,
                    ExpiresAt = DateTime.UtcNow,
                    ErrorMessage = ex.Message
                };
            }
        }

        private string GenerateToken(int userId, string role,int cartId , int wishlistId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim("CartId", cartId.ToString()),
                new Claim("WishlistId", wishlistId.ToString())


            };

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));


            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWT:ValidIssuer"],
                Audience = configuration["JWT:ValidAudience"],
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

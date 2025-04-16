using Microsoft.Extensions.Configuration;
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
            var user = await _authRepository.Login(query.Email, query.Password);

            if (user == null)
            {
                return null;
            }
            int cartId = 0;  // افتراضياً سيكون CartId صفر إذا لم توجد سلة

            if (user is Customer customer && customer.Carts != null && customer.Carts.Any())
            {
                cartId = customer.Carts.First().Id;  // استخدام أول سلة للمستخدم
            }
            AuthenticationModel authenticationModel = new AuthenticationModel
            {
                AccessToken = GenerateToken(user.Id, user.Role.Name , cartId),
                ExpiresAt = DateTime.Now.AddHours(1)
            };

            return authenticationModel;
        }
        private string GenerateToken(int userId, string role,int cartId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                 new Claim("CartId", cartId.ToString()) // إضافة CartId هنا


            };

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));


            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWT:ValidIssuer"],
                Audience = configuration["JWT:ValidAudience"],
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecipeManagementSystemInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemInfrastructure.Implementation
{
    public class Token
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public Token(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> CreateAccessToken(ApplicationUser userData)
        {
            var roles = await _userManager.GetRolesAsync(userData);

            var authorizationClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", userData.Id.ToString()),
                new Claim("Email", userData.Email.ToString()),
                new Claim("UserName", userData.UserName.ToString())
            };

            foreach (var role in roles)
            {
                authorizationClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: authorizationClaims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return accessToken;
        }

        public string CreateRefreshToken()
        {
            string refreshToken;
            do
            {
                var tokenBytes = RandomNumberGenerator.GetBytes(64);
                refreshToken = Convert.ToBase64String(tokenBytes);

            }while(_userManager.Users.Any(a => a.RefreshToken == refreshToken));
            return refreshToken;
        }

    }
}

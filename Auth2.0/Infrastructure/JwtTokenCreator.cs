using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth2._0.Infrastructure
{
    public class JwtTokenCreator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenCreator(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        public string Generate(string email, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow + _jwtSettings.Expire,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Planitly.Backend.Models;

namespace Planitly.Backend.Services
{
    public interface IJwtService
    {
        public string CreateJwt(User user);
    }

    public class JwtService : IJwtService
    {
        private IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            this._config = config;
        }

        public string CreateJwt(User user)
        {
            var claims = new[]
              {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("role", user.Role),
                };

            var jwtSecret = _config.GetValue<string>("JwtSecret");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(String.Empty,
                          String.Empty,
                          claims,
                          expires: DateTime.Now.AddDays(1),
                          signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
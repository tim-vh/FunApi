using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            string token;

            token = GenerateToken(username);

            return Ok(token);
        }

        private static string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Startup.Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securitytoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securitytoken);
            return token;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

            //token = Old(username);

            //if (token == null)
            //{
            //    return Unauthorized();
            //}

            token = GenerateToken();

            return Ok(token);
        }

        private static string GenerateToken()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "userid"),
                new Claim(ClaimTypes.Name, "Tim"),
                new Claim(ClaimTypes.Email, "tim@email.test"),
                new Claim(JwtRegisteredClaimNames.Exp,
                new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
            };

            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("ThisKeyMustBeAtLeast16Characters")),
                        SecurityAlgorithms.HmacSha256));

            var payload = new JwtPayload(claims);

            var token = new JwtSecurityToken(header, payload);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string Old(string username)
        {
            //var token = _jWTManager.Authenticate(usersdata);

            //if (!UsersRecords.Any(x => x.Key == users.Name && x.Value == users.Password))
            //{
            //    return null;
            //}

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            //var tokenKey = Encoding.UTF8.GetBytes("iconfiguration[\"JWT:Key\"]");
            //var tokenKey = Encoding.UTF8.GetBytes("Test123Test123Test123Test123Test123Test123Test123");
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

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginCredentials loginCredentials)
        {
            var user = await _signInManager.UserManager.FindByNameAsync("Tim");
            var check = await _signInManager.CheckPasswordSignInAsync(user, loginCredentials.Password, false);
            //var result = await _signInManager.PasswordSignInAsync(loginCredentials.Username, loginCredentials.Password, true, false);

            

            //await _signInManager.SignInWithClaimsAsync(new IdentityUser(), true, Enumerable.Empty<Claim>());

            var userprincipal = await _signInManager.CreateUserPrincipalAsync(user);
            


            //var token = GenerateToken(loginCredentials.Username);
            var token = GenerateTokenFromClaims(userprincipal);
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

        private static string GenerateTokenFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsPrincipal.Claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Startup.Key), SecurityAlgorithms.HmacSha256Signature),    
                
            };

            

            var securitytoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securitytoken);
            return token;
        }

        public class LoginCredentials
        {
            public string Username { get; set; }

            public string Password { get; set; }
        }
    }
}

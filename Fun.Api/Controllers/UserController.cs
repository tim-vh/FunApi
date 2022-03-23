using Fun.Api.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginCredentials loginCredentials)
        {
            var user = await _signInManager.UserManager.FindByNameAsync(loginCredentials.Username);
            if (user != null && user.IsEnabled)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginCredentials.Password, false);

                if (signInResult.Succeeded)
                {
                    var userprincipal = await _signInManager.CreateUserPrincipalAsync(user);

                    var token = GenerateTokenFromClaims(userprincipal);
                    return Ok(token);
                }
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegistration userRegistration)
        {
            var user = new ApplicationUser(userRegistration.Username)
            {
                IsEnabled = false
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);
            
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
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

        public class UserRegistration
        {
            public string Username { get; set; }

            public string Password { get; set; }
        }
    }
}

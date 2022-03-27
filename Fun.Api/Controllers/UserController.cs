using Fun.Api.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private static byte[] Key = Encoding.UTF8.GetBytes("Test123Test123Test123Test123Test123Test123Test123");

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
                IsEnabled = false,
            };

            user.Roles.Add("User");

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

        [Authorize(Roles = "Admin")]
        [HttpPatch("{username}")]
        public async Task<IActionResult> SetIsEnabled(string username, [FromBody] bool isEnabled)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            user.IsEnabled = isEnabled;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        private static string GenerateTokenFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsPrincipal.Claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature),

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

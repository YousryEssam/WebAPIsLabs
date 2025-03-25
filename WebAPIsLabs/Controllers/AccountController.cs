using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIsLabs.DTO;
using WebAPIsLabs.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPIsLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.configuration = config;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterFormDTO user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = user.UserName,
                    Email = user.Email
                };

                IdentityResult result = await userManager.CreateAsync(applicationUser, user.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Create Success");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginFormDTO loginForm)
        {
            if (ModelState.IsValid) {
                ApplicationUser user = await userManager.FindByNameAsync(loginForm.UserName);
                if (user != null) {
                    bool correctPassword = await userManager.CheckPasswordAsync(user, loginForm.Password);
                    if (correctPassword) { 
                        string JIT = Guid.NewGuid().ToString();
                        var userRoles = await userManager.GetRolesAsync(user);

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, JIT));

                        if (userRoles != null) {
                            foreach (var role in userRoles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role));
                            }
                        }

                        SymmetricSecurityKey signinKey =new(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                        SigningCredentials signingCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: configuration["JWT:Iss"],//url service provider
                            audience: configuration["JWT:Aud"],//url service consumer
                            expires: DateTime.Now.AddHours(3),
                            claims: claims,
                            signingCredentials: signingCredentials
                        );
                        return Ok(new
                        {
                            expired = DateTime.Now.AddHours(1),
                            token = new JwtSecurityTokenHandler().WriteToken(myToken)
                        });
                    }
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return BadRequest(ModelState);
        }
    }
}

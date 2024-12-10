using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketSystem.Data;
using TicketSystem.Data.DTO;
using TicketSystem.Data.Models;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _db;
        private readonly SignInManager<Users> _signInManager1;
        public Authentication(UserManager<Users> userManager, IConfiguration configuration, AppDbContext db, SignInManager<Users> signInManager1)
        {
            _configuration = configuration;
            _userManager = userManager;
            _db = db;
            _signInManager1 = signInManager1;
        }
        [HttpPost]
        public async Task<IActionResult> Registiration(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var branch = await _db.Branches.FindAsync(userDto.AssocBranch);
                if (branch == null)
                    return NotFound("This Branch Does Not Exist");
                var user = new Users()
                {
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber,
                    AssocBranch = branch.Id,
                    Status = userDto.Status,
                };
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                    return Ok($"Welcome {user.UserName}");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.EmailOrName) ?? await _userManager.FindByNameAsync(userLoginDto.EmailOrName);
                if (user == null) return NotFound("This User Does Not Exist");
                if (await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                    var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        claims: claims,
                        issuer: _configuration["JWT:Issuer"],
                        audience: _configuration["JWT:Audience"],
                        expires: DateTime.Now.AddHours(2),
                        signingCredentials: sc
                        );
                    var _token = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                    };
                    return Ok(_token);
                }
                return Unauthorized("The Credintials is Not Correct please Try Again Or SignUp");
            }
            return BadRequest(ModelState);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager1.SignOutAsync();
            return Ok();
        }

    }
}

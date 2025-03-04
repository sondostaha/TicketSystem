using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Server.HttpSys;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Authentication(UserManager<Users> userManager, IConfiguration configuration, AppDbContext db,IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userManager = userManager;
            _db = db;
           _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Registiration(UserDto userDto)
        {
            var exsitUser = await _userManager.FindByEmailAsync(userDto.Email) ?? await _userManager.FindByNameAsync(userDto.UserName);
            if (exsitUser != null)
                return BadRequest("This User Already Exist already different Name Or Email");
            var branch = await _db.Branches.FindAsync(userDto.AssocBranch);
            if (branch == null)
                return NotFound("This Branch Does Not Exist");
            
            var user = new Users()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                AssocBranch =1,
                Status = userDto.Status,
                DepartmentId = userDto.DepartmentId,
            };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
                return Ok($"Welcome {user.UserName}");
 
            return BadRequest(result.Errors.ToList());
        }
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.EmailOrName) ?? await _userManager.FindByNameAsync(userLoginDto.EmailOrName);
                if (user == null) return NotFound("This User Does Not Exist");
                if(await _userManager.IsLockedOutAsync(user))
                {
                    return Unauthorized("Your account is locked due to multiple failed login attempts. Please try again later.");

                }
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
                else
                {
                    // Increment the failed login attempts after a failed login attempt
                    await _userManager.AccessFailedAsync(user);

                    // Check if the account is locked after the failed attempt
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        return Unauthorized("Your account is locked due to multiple failed login attempts. Please try again later.");
                    }

                    return Unauthorized("The Credentials are incorrect. Please try again or sign up.");
                }
            }
            return BadRequest(ModelState);
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            //await _httpContextAccessor.HttpContext.SignOutAsync();
            var token = Request.Headers["Authorization"].FirstOrDefault();
            //if(!string.IsNullOrEmpty(token)) await _tokenBlacklistService.AddTokenToBlacklist(token);
            return Ok("Logged out successfully.");
        }

    }
}

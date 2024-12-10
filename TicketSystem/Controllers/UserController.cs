using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Data.DTO;
using TicketSystem.Data.Models;
using TicketSystem.Migrations;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Director)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserData _getUserData;
        public UserController(
            UserManager<Users> userManager, 
            RoleManager<IdentityRole> roleManager, 
            AppDbContext db, 
            IHttpContextAccessor httpContextAccessor,
            IUserData getUserData
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _getUserData = getUserData;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.Select(x => new
            {
                Name = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Branch = x.Branches,
            }).ToListAsync() ;
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = _userManager.Users.Where(x => x.Id == id).Select(x => new
            {
                Name = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Branch = x.Branches,
            });
            if (user == null)
                return NotFound("This User Does Not Exist");

            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm] UserDto userDto)
        {
            var userExist = await _userManager.FindByEmailAsync(userDto.Email);
            var userExistName = await _userManager.FindByNameAsync(userDto.UserName);

            if (userExist != null && userExistName != null)
                return BadRequest("This User Alread Exist");
            var department = await _db.Departments.FindAsync(userDto.DepartmentId);
            if (department == null)
                return NotFound("This Department Does Not exist");
            var branch = await _db.Branches.FindAsync(userDto.AssocBranch);
            if (branch == null)
                return NotFound("This Branch Does Not Exist");
            var user = new Users()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                AssocBranch = branch.Id,
                DepartmentId = department.Id
            };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var currentUser = _getUserData.GetUserDataFromToken(token).Result;
            if (!result.Succeeded)
                return BadRequest("User creation failed! Please check user details and try again.");
            foreach (var role in currentUser.Role)
            {
                if (role.Equals(UserRoles.Admin))
                    await _userManager.AddToRoleAsync(user, UserRoles.Director);
                else if (role.Equals(UserRoles.Director))
                    await _userManager.AddToRoleAsync(user, UserRoles.Employee);
                else
                    return BadRequest("UnAuthorization");
            }
            return Ok($"User Added Successfully {user}");

        }
        //[HttpPost("{id}")]
        //public async Task<IActionResult> UpdateTicket(EditUserDto userDto,string id)
        //{
        //    var userExist = await _userManager.FindByIdAsync(id);
           
        //    if (userExist == null)
        //        return BadRequest("This User Does Not Exist");
        //    var department = await _db.Departments.FindAsync(userDto.DepartmentId);
        //    if (department == null)
        //        return NotFound("This Department Does Not Exist");
        //    var branch = await _db.Branches.FindAsync(userDto.AssocBranch);
        //    if (branch == null)
        //        return NotFound("This Branch Does Not Exist");
        //    userExist.UserName = userDto.UserName;
        //    userExist.Email = userDto.Email;
        //    userExist.PhoneNumber = userDto.PhoneNumber;
        //    userExist.AssocBranch = branch.Id;
        //    userExist.DepartmentId = department.Id;
        //    if(userDto.Password != null)
        //    {
        //        var hash = new PasswordHasher<Users>();
        //        userExist.PasswordHash = hash.HashPassword(null, userDto.Password);
        //    }


        //}
    }
}

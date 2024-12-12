using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using TicketSystem.Data;
using TicketSystem.Data.DTO;
using TicketSystem.Data.Models;
using TicketSystem.Migrations;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserData _userData;
        private readonly UserManager<Users> _userManager;
        public TicketController(AppDbContext db, IHttpContextAccessor httpContext, IUserData userData,UserManager<Users> userManager)
        {
            _contextAccessor = httpContext;
            _db = db;
            _userData = userData;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetTickets([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var tickets = await _db.Tickets.Select(x => new
            {
                Title = x.Title,
                user = x.Users.UserName,
                creator = x.Creators.UserName,
                Description = x.Description,
                ProgressIndicators = x.ProgressIndicators.ToString(),
                Status = x.Status.ToString(),
            }).Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int totalCount = await _db.Tickets.CountAsync();

            var paginationResponse = new
            {
                Items = tickets,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            return Ok(paginationResponse);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserTickets()
        {
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            var currentUser = await _userData.GetUserDataFromToken(token);
            var user = await _db.Users.FindAsync(currentUser.Id);
            var ticket = await _db.Tickets.Where(x => x.UserId == user.Id).Select(x => new
            {
                Title = x.Title,
                user = x.Users.UserName,
                creator = x.Creators.UserName,
                Description = x.Description,
                ProgressIndicators = x.ProgressIndicators.ToString(),
                Status = x.Status.ToString(),
            }).ToListAsync();
            if (!ticket.Any())
                return Ok("You Do Not Have Any Tickts");
            return Ok(ticket);
        }
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Director)]
        [HttpGet("[action]")]
        public async Task<IActionResult>  GetCreatorTickets()
        {
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            var currentUser = await _userData.GetUserDataFromToken(token);
            var ticket = await _db.Tickets.Where(x => x.CreatorId == currentUser.Id).Select(x => new
            {
                Title = x.Title,
                user = x.Users.UserName,
                creator = x.Creators.UserName,
                Description = x.Description,
                ProgressIndicators = x.ProgressIndicators.ToString(),
                Status = x.Status.ToString(),
            }).ToListAsync();
            if (!ticket.Any())
                return Ok("You Do Not Have Any Tickts");
            return Ok(ticket);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _db.Tickets.Where(x => x.Id == id).Select(x => new
            {
                Title = x.Title,
                user = x.Users.UserName,
                creator = x.Creators.UserName,
                Description = x.Description,
                ProgressIndicators = x.ProgressIndicators.ToString(),
                Status = x.Status.ToString(),
            }).ToListAsync();
            if (ticket == null)
                return NotFound("This Ticket DoesNot Exist");
            return Ok(ticket);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetDepartmentTickets(int id)
        {
            var department = await _db.Departments.FindAsync(id);
            if (department == null)
                return NotFound("This Department Does Not Exist");
            var users = await _db.Users.Where(c => c.DepartmentId == department.Id).Select(x => x.Id).ToListAsync();
            

            var ticket = await _db.Tickets.Where(x => users.Contains(x.UserId)).Select(x => new
            {
                Title = x.Title,
                user = x.Users.UserName,
                creator = x.Creators.UserName,
                Description = x.Description,
                ProgressIndicators = x.ProgressIndicators.ToString(),
                Status = x.Status.ToString(),
            }).ToListAsync();
            if (!ticket.Any())
                return Ok("There Is No Tickets Avaliable");

            return Ok(ticket);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAcceptedTickets()
        {
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            var currentUser = await _userData.GetUserDataFromToken(token);
            var ticket = await _db.Tickets.Where(x => x.Status == Status.Accepted).Select(x => new
            {
                Title = x.Title,
                user = x.Users.UserName,
                creator = x.Creators.UserName,
                Description = x.Description,
                ProgressIndicators = x.ProgressIndicators.ToString(),
                Status = x.Status.ToString(),
            }).ToListAsync();
            if (!ticket.Any())
                return Ok("You Do Not Have Any Tickts");
            return Ok(ticket);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetpendingTickets()
        {
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            var currentUser = await _userData.GetUserDataFromToken(token);
            var ticket = await _db.Tickets.Where(x => x.Status == Status.pending).Select(x => new
            {
                Title = x.Title,
                user = x.Users.UserName,
                creator = x.Creators.UserName,
                Description = x.Description,
                ProgressIndicators = x.ProgressIndicators.ToString(),
                Status = x.Status.ToString(),
            }).ToListAsync();
            if (!ticket.Any())
                return Ok("There Is No Tickets Avaliable");

            return Ok(ticket);
        }
        [HttpGet("[action]")]

        public async Task<IActionResult> GetRejectedTickets()
        {
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            var currentUser = await _userData.GetUserDataFromToken(token);
            var ticket = await _db.Tickets.Where(x => x.Status == Status.Rejected).Select(x => new
            {
                Title = x.Title,
                user = x.Users.UserName,
                //creator = x.Creators,
                Description = x.Description,
                ProgressIndicators = x.ProgressIndicators.ToString(),
                Status = x.Status.ToString(),
            }).ToListAsync();
            if (!ticket.Any())
                return Ok("There Is No Tickets Avaliable");
            return Ok(ticket);
        }
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Director)]
        [HttpPost]
        public async Task<IActionResult> AddTicket([FromForm] TicketsDto ticketsDto)
        {
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            var currentUserData = await _userData.GetUserDataFromToken(token);
            var user = await _db.Users.FindAsync(ticketsDto.UsersId);
            if (user == null)
                return NotFound("This User Does Not Exist");
            var currentUser = await _db.Users.FindAsync(currentUserData.Id);
            var userRole = await _userManager.GetRolesAsync(user);
            var roleAdminAndDirector = new List<string> { UserRoles.Admin, UserRoles.Director };
            var ticket = new Tickets()
            {
                Title = ticketsDto.Title,
                Description = ticketsDto.Description,
                UserId =  user.Id,
                CreatorId = currentUser.Id,
                ProgressIndicators = ticketsDto.ProgressIndicators,
                Status = ticketsDto.Status,
            };
            if (currentUser.Departments.Title != user.Departments.Title && !userRole.Any(u => roleAdminAndDirector.Contains(u)))
            {
                return Unauthorized("You Can Not Add Ticket To Diferrent Department Employee ");
            } else if (!userRole.Any(u => roleAdminAndDirector.Contains(u)) || userRole.Any(u => roleAdminAndDirector.Contains(u)))
            {
                await _db.Tickets.AddAsync(ticket);
                await _db.SaveChangesAsync();
                return Ok("Ticket Added Successfully");
            }
            return BadRequest(ModelState);

        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateTicket(TicketsDto ticketsDto,int id)
        {
            var ticket = await _db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound("This Ticket Does Not Exist");
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            var currentUserData = await _userData.GetUserDataFromToken(token);
            var user = await _db.Users.FindAsync(ticketsDto.UsersId);
            if (user == null)
                return NotFound("This User Does Not Exist");
            var currentUser = await _db.Users.FindAsync(currentUserData.Id);
            var userRole = await _userManager.GetRolesAsync(user);
            var roleAdminAndDirector = new List<string> { UserRoles.Admin, UserRoles.Director };
           
            if (currentUser.Departments.Title != user.Departments.Title && !userRole.Any(u => roleAdminAndDirector.Contains(u)))
            {
                return Unauthorized("You Can Not Add Ticket To Diferrent Department Employee ");
            }
            else if (!userRole.Any(u => roleAdminAndDirector.Contains(u)) || userRole.Any(u => roleAdminAndDirector.Contains(u)))
            {
                ticket.Title = ticketsDto.Title ?? ticket.Title;
                ticket.Description = ticketsDto.Description ?? ticket.Description;
                ticket.Status = ticketsDto.Status;
                ticket.ProgressIndicators = ticketsDto.ProgressIndicators;
                ticket.UserId = user.Id;
                ticket.CreatorId = currentUserData.Id;
                ticket.UpdatedAt = DateTime.Now;
                return Ok("Ticket Updated Successfully");
            }
            return BadRequest(ModelState);

            
        }
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound("This Ticket Does Not Exist");
            _db.Remove(ticket);
            await _db.SaveChangesAsync();
            return Ok("Ticket Deleted Successfully");
        }
    }

}

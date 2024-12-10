using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Data.DTO;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _identityRole;
        //private readonly
        public RolesController(RoleManager<IdentityRole> identityRole)
        {
            _identityRole = identityRole;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _identityRole.Roles.ToListAsync();
            if (roles == null)
                return Ok("There Is No Roles");
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var role = await _identityRole.Roles.Where(x => x.Id == id).Select(x => new
            {
                RoleName = x.Name,
            }).ToListAsync();
            if (role == null)
                return NotFound("This Role Does Not Exist");
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoles(RoleDto roleDto)
        {
            var role = new IdentityRole()
            {
                Name = roleDto.Name,
            };
            var result = await _identityRole.CreateAsync(role);
            if (result.Succeeded)
                return Ok("Role Added Successfully");
            return BadRequest();

        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateRole(RoleDto roleDto, string id)
        {
            var role = await _identityRole.FindByIdAsync(id);
            if (role == null)
                return NotFound("This Role Does Not Exist");
            role.Name = roleDto.Name;
            var result = await _identityRole.UpdateAsync(role);
            if (result.Succeeded)
                return Ok("Role Updated Successfully");
            return BadRequest(result.Errors);
        }
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _identityRole.FindByIdAsync(id);
            if (role == null)
                return NotFound("This Role Does Not Exist");
            var result = await _identityRole.DeleteAsync(role);
            if (result.Succeeded)
                return Ok("This Role Deleted Successfully");
            return BadRequest(result.Errors);
        }
    }
}

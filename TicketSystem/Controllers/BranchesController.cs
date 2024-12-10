using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Data;
using TicketSystem.Data.DTO;
using TicketSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public BranchesController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetBranches()
        {

            var branches = await _db.Branches.Select(x => new
            {
                Title = x.Title,
                Address = x.Address,

            }).ToListAsync();
            if (branches.Any())
                return Ok(branches);
            return Ok("There Is No Branches Added Yet");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(int id)
        {
            var branch = await _db.Branches.Where(x => x.Id == id).Select(x => new
            {
                Title = x.Title,
                Address = x.Address,

            }).FirstAsync();
            if (branch == null)
                return NotFound("This Branch Does Not Exist");
            return Ok(branch);
        }
        [HttpPost]
        public async Task<IActionResult> AddBranch(BranchDto branchDto)
        {
            var branch = new Branches()
            {
                Title = branchDto.Title,
                Address = branchDto.Address,

            };
            await _db.Branches.AddAsync(branch);
            await _db.SaveChangesAsync();
            return Ok("Branch Added Successfully");
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateBranch(BranchDto branchDto, int id)
        {
            var branch = await _db.Branches.FindAsync(id);
            if (branch == null) return NotFound("This Branch Does Not Exist");
            branch.Title = branchDto.Title ?? branch.Title;
            branch.Address = branchDto.Address ?? branch.Address;

            await _db.SaveChangesAsync();
            return Ok("Branch Updated Sucessfully");
        }
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var branch = await _db.Branches.FindAsync(id);
            if (branch == null) return NotFound("This Branch Does Not Exist");
            _db.Remove(branch);
            await _db.SaveChangesAsync();
            return Ok("Branch Deleted Successfully");
        }
    }
}

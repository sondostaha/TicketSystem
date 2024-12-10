using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
//using WebPOS.Models;

namespace TicketSystem.Data.Models
{
    public class Users : IdentityUser
    {

        [ForeignKey(nameof(Branches))]
        public int AssocBranch { get; set; }
        public virtual Branches? Branches { get; set; }

        [ForeignKey(nameof(Departments))]
        public int DepartmentId { get; set; }
        public virtual Departments? Departments { get; set; }
        public byte? Status { get; set; } = 1;  
        //public virtual ICollection<Tickets>? Tickets { get; set; }
    }
}

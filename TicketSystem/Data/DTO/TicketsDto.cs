using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketSystem.Data.Models;

namespace TicketSystem.Data.DTO
{

    public class TicketsDto
    {
        [Required(ErrorMessage = "User Title is required"), MaxLength(50, ErrorMessage = "{0} length must be between {2} and {1}."), MinLength(6)]

        public string Title { get; set; }
        [MaxLength(500, ErrorMessage = "{0} length must be between {2} and {1}.")]

        public string? Description { get; set; }

        public string UsersId { get; set; }

        //public Progress ProgressIndicators { get; set; } = Progress.One;
        //public Status Status { get; set; } = Status.pending;

    }
}

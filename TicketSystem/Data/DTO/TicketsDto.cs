using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketSystem.Data.Models;

namespace TicketSystem.Data.DTO
{

    public class TicketsDto
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }

        public string UsersId { get; set; }

        public Progress ProgressIndicators { get; set; } = Progress.One;
        public Status Status { get; set; } = Status.pending;

    }
}

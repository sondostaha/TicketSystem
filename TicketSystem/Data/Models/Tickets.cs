using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Data.Models
{
    public enum Progress
    {
        One = 0, Two = 1, Three = 2, Four = 3, Five = 4
    }
    public enum Status
    {
       Created = 0, Accepted = 1, pending = 2, Rejected = 3, AwaitingReview = 4
    }
    public class Tickets
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(Users))]
        public string UserId { get; set; }
        public virtual Users? Users { get; set; }

        public Progress ProgressIndicators { get; set; } = Progress.One;
        [ForeignKey(nameof(Creators))]
        public string CreatorId { get; set; }
        public virtual Users? Creators { get; set; }

        public Status Status { get; set; } = Status.pending;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

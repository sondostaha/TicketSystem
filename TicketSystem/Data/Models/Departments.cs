namespace TicketSystem.Data.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Users>? Users { get; set; }
    }
}

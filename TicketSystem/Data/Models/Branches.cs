﻿namespace TicketSystem.Data.Models
{
    public class Branches
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
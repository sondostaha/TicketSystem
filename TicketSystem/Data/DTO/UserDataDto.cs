﻿namespace TicketSystem.Data.DTO
{
    public class UserDataDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Role { get; set; }
    }
}

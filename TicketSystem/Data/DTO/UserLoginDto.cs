using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Data.DTO
{
    public class UserLoginDto
    {
        [Required]
        public string EmailOrName { get; set; }
        //public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
 
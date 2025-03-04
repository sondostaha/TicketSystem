using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Data.DTO
{
    public class UserDataDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "User Name is required"), MaxLength(100, ErrorMessage = "{0} length must be between {2} and {1}."), MinLength(6)]

        public string Name { get; set; }

        [Required(ErrorMessage = "User Email is required"), MaxLength(50, ErrorMessage = "{0} length must be between {2} and {1}."),EmailAddress]

        public string Email { get; set; }
        
        public List<string> Role { get; set; }
    }
}

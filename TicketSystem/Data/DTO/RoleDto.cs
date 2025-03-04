using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Data.DTO
{
    public class RoleDto
    {
        [Required(ErrorMessage = "Name is required"), MaxLength(50, ErrorMessage = "{0} length must be between {2} and {1}."), MinLength(6)]

        public string  Name { get; set; }
        //public string NormalizedName { get; set; }

    }
}

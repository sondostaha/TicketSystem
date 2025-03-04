using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Data.DTO
{
    public class BranchDto
    {
        [Required(ErrorMessage = "Title is required"), MaxLength(50, ErrorMessage = "{0} length must be between {2} and {1}."), MinLength(6)]

        public string Title { get; set; }
        [Required(ErrorMessage = " Address is required"), MaxLength(100, ErrorMessage = "{0} length must be between {2} and {1}."), MinLength(6)]

        public string Address { get; set; }
    }
}

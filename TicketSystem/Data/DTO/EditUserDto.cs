using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TicketSystem.Data.DTO
{

    public class EditUserDto
    {
        [Required(ErrorMessage = "User Name is required"),MaxLength(100,ErrorMessage = "{0} can have a max of {1} characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "User Password is required"), MinLength(6, ErrorMessage = "Min character Is 6")]
        public string Password { get; set; }
        [Required(ErrorMessage = "User Email is required"), EmailAddress(ErrorMessage = "This Must be a email")]
        public string Email { get; set; }
        [Range(0, 11, ErrorMessage = "Phone cannot be more that 11")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "User Branch is required")]
        public int AssocBranch { get; set; }
        public int DepartmentId {  get; set; }
        public byte? Status { get; set; } = 1;

    }

}

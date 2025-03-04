using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TicketSystem.Data.DTO
{
    //[JsonConverter(typeof(StringEnumConverter))]
    //public enum  UserRoles 
    //{
    //    Employee = 0,
    //    Admin =1,
    //    Director =2,
       
    //}
    public class UserDto
    {//maxlen
        [Required(ErrorMessage = "User Name is required"), MaxLength(100,ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "User Password is required"), MaxLength(50, ErrorMessage = "{0} length must be between {2} and {1}."),MinLength(6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "User Email is required"),EmailAddress(ErrorMessage ="This Must be a email")]
        public string Email { get; set; }
        [MaxLength(11, ErrorMessage = "Phone cannot be more that 11"),MinLength(11),Phone]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "User Branch is required")]
        public int AssocBranch { get; set; }
        public int DepartmentId {  get; set; }
        public byte? Status { get; set; } = 1;

    }
    public class UserRoles
    {
        public const string Admin ="Admin";
        public const string Employee = "Employee";
        public const string Director = "Director";
    }

}

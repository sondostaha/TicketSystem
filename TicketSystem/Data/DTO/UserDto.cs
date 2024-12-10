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
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "User Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "User Email is required")]
        public string Email { get; set; }
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

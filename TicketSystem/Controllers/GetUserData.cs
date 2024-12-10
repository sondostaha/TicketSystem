using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicketSystem.Data.DTO;
using TicketSystem.Data.Models;

namespace TicketSystem.Controllers
{
    public interface IUserData
    {
        Task<UserDataDto> GetUserDataFromToken(string token);
    }
    public class GetUserData : IUserData
    {
        public async Task<UserDataDto> GetUserDataFromToken(string token)
        {
            var tokenHandeler = new JwtSecurityTokenHandler();
            try
            {
                var securityToken = tokenHandeler.ReadJwtToken(token);
                var claims = securityToken.Claims;
                var user = new UserDataDto()
                {
                    Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    Id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                    Name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                    Role = claims.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).ToList(),

                };
                return user;
            }
            catch (SecurityTokenException ex)
            {
                return null;
            }
        }
    }
}

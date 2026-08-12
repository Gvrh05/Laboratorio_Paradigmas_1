using System.Threading.Tasks;
using LibraryService.WebAPI.Application.DTOs;

namespace LibraryService.WebAPI.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> AuthenticateAsync(string email, string password);
    }
}
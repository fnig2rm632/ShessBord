using System.Threading.Tasks;
using ShessBord.DTO.User;

namespace ShessBord.Interfaces;

public interface IAppUserService
{
    Task<UserRequestDto?> GetUserProfileAsync(string userId);
    
    Task<UserRequestDto?> GetUserAsync(string userId);
    
    Task<UserRequestDto?> GetMainUserAsync();
}
using System.Threading.Tasks;
using ShessBord.DTO.User;

namespace ShessBord.Interfaces;

public interface IUserApiClient
{
    Task<UserRequestDto?> GetUser(string userId, string accessToken = null);
    Task<UserRequestDto?> GetUserProfile(string userId, string accessToken = null);
}
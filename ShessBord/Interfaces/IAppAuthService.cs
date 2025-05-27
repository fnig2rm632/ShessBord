using System.Threading.Tasks;

namespace ShessBord.Interfaces;

public interface IAppAuthService
{
    Task<bool> TryAutoLoginAsync();
    Task LogoutAsync();
}
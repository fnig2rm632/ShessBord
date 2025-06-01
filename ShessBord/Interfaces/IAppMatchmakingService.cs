using System.Threading.Tasks;
using ShessBord.DTO.Game;
using ShessBord.DTO.User;

namespace ShessBord.Interfaces;

public interface IAppMatchmakingService
{
    Task<bool> StartSearch(string type, int size);
    Task<bool> Search(string type, int size);
    Task<bool> Cancel();
    Task<UserRequestDto> Start();
}

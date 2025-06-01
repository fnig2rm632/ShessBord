using System.Threading.Tasks;

namespace ShessBord.Interfaces;

public interface IGameClient
{
    Task ConnectAsync();
    Task SendMoveAsync(int x, int y);
    Task DisconnectAsync();
}
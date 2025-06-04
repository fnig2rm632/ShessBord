using System;
using System.Threading.Tasks;
using ShessBord.Models;

namespace ShessBord.Interfaces;

public interface IGameWebSocketService
{
    Task ConnectAsync(Guid playerId);
    Task SendMoveAsync(GameMove move);
    IObservable<GameStatus> GameUpdates { get; }
    Task DisconnectAsync();
    void Disconnect();
}
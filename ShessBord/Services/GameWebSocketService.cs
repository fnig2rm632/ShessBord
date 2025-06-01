using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.Services;
using System;
using System.Net.WebSockets;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class GameWebSocketService : IGameWebSocketService, IDisposable
{
    private readonly string _serverUrl = "ws://localhost:5160";
    private ClientWebSocket _webSocket;
    private readonly Subject<GameStatus> _gameUpdates = new();
    private CancellationTokenSource _cts;
    private bool _disposed;

    public IObservable<GameStatus> GameUpdates => _gameUpdates;

    public GameWebSocketService()
    {
        _webSocket = new ClientWebSocket();
        _cts = new CancellationTokenSource();
    }

    public async Task ConnectAsync(Guid playerId)
    {
        if (_webSocket.State == WebSocketState.Open)
            await DisconnectAsync();
        
        var uri = new Uri($"{_serverUrl}/game-ws?playerId={playerId}");
        await _webSocket.ConnectAsync(uri, _cts.Token);
        _ = ReceiveMessagesAsync();
    }

    public async Task SendMoveAsync(GameMove move)
    {
        if (_webSocket?.State != WebSocketState.Open)
            throw new InvalidOperationException("WebSocket is not connected");
        
        var json = JsonConvert.SerializeObject(move);
        var buffer = Encoding.UTF8.GetBytes(json);
        await _webSocket.SendAsync(
            new ArraySegment<byte>(buffer),
            WebSocketMessageType.Text,
            true,
            _cts.Token
        );
    }

    private async Task ReceiveMessagesAsync()
    {
        var buffer = new byte[1024 * 4];
        try
        {
            while (_webSocket.State == WebSocketState.Open && !_cts.IsCancellationRequested)
            {
                var result = await _webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    _cts.Token
                );

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var gameStatus = JsonConvert.DeserializeObject<GameStatus>(json);
                    _gameUpdates.OnNext(gameStatus);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await DisconnectAsync();
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Нормальное завершение при отмене
        }
        catch (Exception ex)
        {
            _gameUpdates.OnError(ex);
        }
    }

    public void Disconnect()
    {
        _cts.Cancel();
        _webSocket?.Dispose();
        _gameUpdates.OnCompleted();
    }
    
    public async Task DisconnectAsync()
    {
        if (_webSocket == null || _disposed)
            return;

        try
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                await _webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Client disconnect",
                    _cts.Token
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during disconnect: {ex.Message}");
        }
        finally
        {
            _webSocket.Dispose();
            _cts.Cancel();
            _gameUpdates.OnCompleted();
            _disposed = true;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        Disconnect();
        _gameUpdates.Dispose();
        _disposed = true;
    }
}
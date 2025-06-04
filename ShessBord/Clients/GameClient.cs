using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.Clients;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class GameClient : IGameClient
{
    private readonly string _serverUrl;
    private readonly Guid _playerId;
    private ClientWebSocket _webSocket;

    public GameClient(string serverUrl, Guid playerId)
    {
        _serverUrl = "ws://192.168.1.106:5160";
        _playerId = playerId;
        _webSocket = new ClientWebSocket();
    }

    // Подключение к серверу
    public async Task ConnectAsync()
    {
        var uri = new Uri($"{_serverUrl}/game-ws?playerId={_playerId}");
        await _webSocket.ConnectAsync(uri, CancellationToken.None);
        Console.WriteLine($"Connected to {uri}");

        // Запускаем фоновую задачу для получения сообщений
        _ = Task.Run(ReceiveMessagesAsync);
    }

    // Отправка хода на сервер
    public async Task SendMoveAsync(int x, int y)
    {
        var move = new GameMove
        {
            IdPlayer = _playerId,
            X = x,
            Y = y
        };

        var json = JsonConvert.SerializeObject(move);
        var buffer = Encoding.UTF8.GetBytes(json);

        await _webSocket.SendAsync(
            new ArraySegment<byte>(buffer),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None
        );

        Console.WriteLine($"Sent move: X={x}, Y={y}");
    }

    // Получение сообщений от сервера
    private async Task ReceiveMessagesAsync()
    {
        var buffer = new byte[1024 * 4];

        try
        {
            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    CancellationToken.None
                );

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var gameStatus = JsonConvert.DeserializeObject<GameStatus>(json);

                    Console.WriteLine("Received game state:");
                    Console.WriteLine($"- Active player: {gameStatus.ActivePlayerId}");
                    Console.WriteLine($"- Current move: {gameStatus.CurrentMove}");
                    //Console.WriteLine($"- Field matrix: {gameStatus.FieldMatrix[0, 0]} ...");
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "Closed by server",
                        CancellationToken.None
                    );
                    Console.WriteLine("Disconnected by server");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WebSocket error: {ex.Message}");
        }
    }

    // Закрытие соединения
    public async Task DisconnectAsync()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            await _webSocket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Closed by client",
                CancellationToken.None
            );
        }
        _webSocket.Dispose();
        Console.WriteLine("Disconnected");
    }
}
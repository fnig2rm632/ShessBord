using System;
using ReactiveUI;
using ShessBord.Interfaces;

namespace ShessBord.DTO.Game;

public class GameResponseDto
{
    public int Id { get; set; }
    public int BoardSize { get; set; } // 9, 13, 19
    public string PlayerWhiteId { get; set; } = null!;
    public string PlayerBlackId { get; set; } = null!;
    public string? PlayerWhiteName { get; set; } = null!;
    public string? PlayerBlackName { get; set; } = null!;
    public string? PlayerWinId { get; set; } = null!;
    public DateTime? StartTime { get; set; } 
    public DateTime? EndTime { get; set; } 
    public string? Type { get; set; }
    
    // Дополнительно
    public string? SizeBoard => $"{BoardSize}x{BoardSize}";
    public TimeSpan? Time => EndTime - StartTime;
    
}
using System;
using System.Collections.Generic;

namespace ShessBord.Models;

public class GameStatus
{
    public Guid GameId { get; set; }
    public Guid Player1Id { get; set; }
    public Guid Player2Id { get; set; }
    public Char[,] FieldMatrix { get; set; } 
    public List<GameMove> MoveHistory { get; set; } 
    public Guid ActivePlayerId { get; set; } 
    public int CurrentMove { get; set; } 
}
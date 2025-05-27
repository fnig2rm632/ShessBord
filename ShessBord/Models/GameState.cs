using System;
using System.Collections.Generic;

namespace ShessBord.Models;

public class GameState
{
    public GameMode Mode { get; set; }
    public StoneColor CurrentPlayer { get; set; }
    public GameTimeSettings TimeSettings { get; set; }
    public Dictionary<StoneColor, int> Captures { get; } = new();
    public Stack<Move> MoveHistory { get; } = new();
    public Stack<ulong> PositionHistory { get; } = new();
    public bool IsAiThinking { get; set; }
    public GameScore FinalScore { get; set; }
    public StoneColor? Winner { get; set; }
    public int ConsecutivePasses { get; set; }
}

public class GameStateEventArgs : EventArgs
{
    public Board Board { get; }
    public GameState State { get; }
    
    public GameStateEventArgs(Board board, GameState state)
    {
        Board = board;
        State = state;
    }
}

public enum GameMode
{
    PlayerVsPlayer,
    Demo
}
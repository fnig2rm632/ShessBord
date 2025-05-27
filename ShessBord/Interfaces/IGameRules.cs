using System;
using System.Collections.Generic;
using ShessBord.Models;

/// <summary>
/// Интерфейс правил игры Го
/// </summary>
public interface IGameRules
{
    bool IsValidMove(Board board, int x, int y, StoneColor currentPlayer);
    bool IsValidMove(Board board, Move move, StoneColor currentPlayer);
    List<Stone> CheckCaptures(Board board, Move move);
    List<Stone> CheckCaptures(Board board, int x, int y);
    bool IsKoViolation(Board board, Move move, GameState state);
    bool IsGameOver(Board board, GameState state);
    GameScore CalculateScore(Board board);
    StoneColor? DetermineWinner(GameScore score);
    bool IsSuicideMove(Board board, Move move);
    List<Stone> FindGroup(Board board, int x, int y);
    int CountLiberties(Board board, List<Stone> group);
}

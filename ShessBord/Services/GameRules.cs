using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShessBord.Models;

namespace ShessBord.Services;

public class GameRules : IGameRules
{
    public bool IsValidMove(Board board, Move move, StoneColor currentPlayer)
    {
        // 1. Проверка, что точка на доске
        if (move.X < 0 || move.X >= board.Size || move.Y < 0 || move.Y >= board.Size)
            return false;
    
        // 2. Проверка, что точка свободна
        if (!board.IsEmpty(move.X, move.Y))
            return false;
    
        // 3. Проверка правила ко
        if (IsKoViolation(board, move, new GameState { CurrentPlayer = currentPlayer }))
            return false;
    
        // 4. Проверка на самоубийственный ход
        if (IsSuicideMove(board, move))
            return false;
    
        return true;
    }
    
    public bool IsValidMove(Board board, int x, int y, StoneColor currentPlayer)
    {
        // Создаем объект Move для передачи в основной метод
        var move = new Move(x, y, currentPlayer);
        return IsValidMove(board, move, currentPlayer);
    }

    public List<Stone> CheckCaptures(Board board, int x, int y)
    {
        var move = new Move(x, y, StoneColor.Black);
        return CheckCaptures(board, move);
    }
    
    public List<Stone> CheckCaptures(Board board, Move move)
    {
        var capturedStones = new List<Stone>();
        var opponentColor = move.Color.Opposite();
        
        // Проверяем все соседние точки противника
        foreach (var (nx, ny) in board.GetNeighbors(move.X, move.Y))
        {
            var neighbor = board[nx, ny];
            if (neighbor != null && neighbor.Color == opponentColor)
            {
                var group = FindGroup(board, nx, ny);
                if (CountLiberties(board, group) == 0)
                {
                    capturedStones.AddRange(group);
                }
            }
        }
        
        return capturedStones;
    }

    public bool IsKoViolation(Board board, Move move, GameState state)
    {
        // Создаем временную копию доски для проверки
        var testBoard = board.DeepCopy();
        testBoard.PlaceStone(move.X, move.Y, move.Color);
        
        // Удаляем захваченные камни
        var captures = CheckCaptures(testBoard, move);
        foreach (var stone in captures)
        {
            testBoard.RemoveStone(stone.X, stone.Y);
        }
        
        // Получаем хеш новой позиции
        var newHash = testBoard.GetPositionHash();
        
        // Проверяем, была ли такая позиция в истории
        return state.PositionHistory.Contains(newHash);
    }

    public bool IsGameOver(Board board, GameState state)
    {
        // 1. Проверка на два последовательных паса
        if (state.ConsecutivePasses >= 2)
            return true;
            
        // 2. Проверка на заполнение доски (опционально)
        if (IsBoardFull(board))
            return true;
            
        // 3. Другие условия завершения игры
        return false;
    }

    public GameScore CalculateScore(Board board)
    {
        var score = new GameScore(); // Komi инициализируется в конструкторе
        var counted = new bool[board.Size, board.Size];
    
        for (int x = 0; x < board.Size; x++)
        {
            for (int y = 0; y < board.Size; y++)
            {
                if (counted[x, y]) continue;
            
                if (board.IsEmpty(x, y))
                {
                    var territory = FindTerritory(board, x, y, counted);
                    if (territory.Owner.HasValue)
                    {
                        score[territory.Owner.Value] += territory.Size;
                    }
                }
                else
                {
                    score[board[x, y].Color] += 1;
                    counted[x, y] = true;
                }
            }
        }
    
        // Добавляем коми
        score.AddKomi(); // Используем специальный метод
    
        return score;
    }

    public StoneColor? DetermineWinner(GameScore score)
    {
        if (score[StoneColor.Black] > score[StoneColor.White])
            return StoneColor.Black;
        if (score[StoneColor.White] > score[StoneColor.Black])
            return StoneColor.White;
        return null; // Ничья (крайне редко в Го)
    }

    public bool IsSuicideMove(Board board, Move move)
    {
        // Временная доска для проверки
        var testBoard = board.DeepCopy();
        testBoard.PlaceStone(move.X, move.Y, move.Color);
        
        // Проверяем, есть ли у новой группы дыхания
        var group = FindGroup(testBoard, move.X, move.Y);
        return CountLiberties(testBoard, group) == 0;
    }

    public List<Stone> FindGroup(Board board, int x, int y)
    {
        if (board.IsEmpty(x, y))
            return new List<Stone>();
            
        var color = board[x, y].Color;
        var group = new List<Stone>();
        var visited = new bool[board.Size, board.Size];
        
        FindConnectedStones(board, x, y, color, group, visited);
        return group;
    }

    public int CountLiberties(Board board, List<Stone> group)
    {
        if (group == null || group.Count == 0)
            return 0;
            
        var liberties = new HashSet<(int, int)>();
        
        foreach (var stone in group)
        {
            foreach (var (nx, ny) in board.GetNeighbors(stone.X, stone.Y))
            {
                if (board.IsEmpty(nx, ny))
                {
                    liberties.Add((nx, ny));
                }
            }
        }
        
        return liberties.Count;
    }

    private void FindConnectedStones(Board board, int x, int y, StoneColor color, 
                                   List<Stone> group, bool[,] visited)
    {
        if (x < 0 || x >= board.Size || y < 0 || y >= board.Size || visited[x, y])
            return;
            
        var stone = board[x, y];
        if (stone == null || stone.Color != color)
            return;
            
        visited[x, y] = true;
        group.Add(stone);
        
        // Рекурсивно проверяем соседей
        FindConnectedStones(board, x-1, y, color, group, visited);
        FindConnectedStones(board, x+1, y, color, group, visited);
        FindConnectedStones(board, x, y-1, color, group, visited);
        FindConnectedStones(board, x, y+1, color, group, visited);
    }

    private TerritoryInfo FindTerritory(Board board, int x, int y, bool[,] counted)
    {
        var territory = new List<(int, int)>();
        var borders = new HashSet<StoneColor>();
        var visited = new bool[board.Size, board.Size];
        
        ExploreTerritory(board, x, y, territory, borders, visited);
        
        // Помечаем точки как подсчитанные
        foreach (var (tx, ty) in territory)
        {
            counted[tx, ty] = true;
        }
        
        // Определяем владельца территории
        if (borders.Count == 1)
        {
            return new TerritoryInfo(borders.First(), territory.Count);
        }
        
        return new TerritoryInfo(null, 0);
    }

    private void ExploreTerritory(Board board, int x, int y, 
                                List<(int, int)> territory, 
                                HashSet<StoneColor> borders, 
                                bool[,] visited)
    {
        if (x < 0 || x >= board.Size || y < 0 || y >= board.Size || visited[x, y])
            return;
            
        if (!board.IsEmpty(x, y))
        {
            borders.Add(board[x, y].Color);
            return;
        }
        
        visited[x, y] = true;
        territory.Add((x, y));
        
        ExploreTerritory(board, x-1, y, territory, borders, visited);
        ExploreTerritory(board, x+1, y, territory, borders, visited);
        ExploreTerritory(board, x, y-1, territory, borders, visited);
        ExploreTerritory(board, x, y+1, territory, borders, visited);
    }

    private bool IsBoardFull(Board board)
    {
        for (int x = 0; x < board.Size; x++)
            for (int y = 0; y < board.Size; y++)
                if (board.IsEmpty(x, y))
                    return false;
                    
        return true;
    }

    private struct TerritoryInfo
    {
        public StoneColor? Owner { get; }
        public int Size { get; }
        
        public TerritoryInfo(StoneColor? owner, int size)
        {
            Owner = owner;
            Size = size;
        }
    }

}

using System;
using System.Collections.Generic;

public class Board
{
    // Двумерный массив для хранения камней
    private readonly Stone[,] _grid;
    
    // Размер доски (9x9, 13x13, 19x19)
    public int Size { get; }
    
    // История позиций для проверки правила "ко"
    private readonly Stack<ulong> _positionHistory = new Stack<ulong>();

    // Конструктор
    public Board(int size)
    {
        if (size != 9 && size != 13 && size != 19)
            throw new ArgumentException("Invalid board size. Must be 9, 13, or 19.");
        
        Size = size;
        _grid = new Stone[size, size];
    }

    // Индексатор для удобного доступа к камням
    public Stone this[int x, int y]
    {
        get
        {
            ValidateCoordinates(x, y);
            return _grid[x, y];
        }
        private set
        {
            ValidateCoordinates(x, y);
            _grid[x, y] = value;
        }
    }

    // Проверка координат
    private void ValidateCoordinates(int x, int y)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size)
            throw new ArgumentOutOfRangeException($"Coordinates ({x}, {y}) are out of bounds.");
    }

    // Размещение камня
    public void PlaceStone(int x, int y, StoneColor color)
    {
        if (this[x, y] != null)
            throw new InvalidOperationException($"Position ({x}, {y}) is already occupied.");
        
        this[x, y] = new Stone(color, x, y);
    }

    // Удаление камня
    public void RemoveStone(int x, int y)
    {
        if (this[x, y] == null)
            throw new InvalidOperationException($"Position ({x}, {y}) is already empty.");
        
        this[x, y] = null;
    }

    // Проверка, свободна ли точка
    public bool IsEmpty(int x, int y) => this[x, y] == null;

    // Получение всех соседних точек (для проверки групп и дыханий)
    public IEnumerable<(int x, int y)> GetNeighbors(int x, int y)
    {
        var directions = new (int dx, int dy)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        
        foreach (var (dx, dy) in directions)
        {
            int nx = x + dx;
            int ny = y + dy;
            
            if (nx >= 0 && nx < Size && ny >= 0 && ny < Size)
                yield return (nx, ny);
        }
    }

    // Копирование доски (для анализа без изменения оригинала)
    public Board DeepCopy()
    {
        var copy = new Board(Size);
        
        for (int x = 0; x < Size; x++)
        for (int y = 0; y < Size; y++)
            if (!IsEmpty(x, y))
                copy.PlaceStone(x, y, this[x, y].Color);
        
        return copy;
    }

    // Хеш позиции для проверки правила "ко"
    public ulong GetPositionHash()
    {
        // Простая хеш-функция (можно заменить на Zobrist hashing)
        ulong hash = 0;
        
        for (int x = 0; x < Size; x++)
        for (int y = 0; y < Size; y++)
        {
            hash = hash * 31 + (ulong)(this[x, y]?.GetHashCode() ?? 0);
        }
        
        return hash;
    }

    // Очистка доски
    public void Clear()
    {
        for (int x = 0; x < Size; x++)
        for (int y = 0; y < Size; y++)
            _grid[x, y] = null;
        
        _positionHistory.Clear();
    }
}

// Цвет камня
public enum StoneColor { Black, White }

// Класс камня
public class Stone
{
    public StoneColor Color { get; }
    public int X { get; }
    public int Y { get; }
    
    public Stone(StoneColor color, int x, int y)
    {
        Color = color;
        X = x;
        Y = y;
    }
    
    // Для отладки
    public override string ToString() => $"{Color} at ({X}, {Y})";
}
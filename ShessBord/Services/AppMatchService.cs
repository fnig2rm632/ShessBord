using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using ShessBord.Controls;
using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.Services;

public class AppMatchService : IAppMatchService
{
    private readonly IGameRules _rules;
    private Board _board;
    private StoneColor _currentPlayer;
    private readonly List<ulong> _positionHistory = new();
    
    public Board CurrentBoard => _board;
    public StoneColor CurrentPlayer => _currentPlayer;
    public StoneColor? GameResult { get; private set; }
    
    private readonly Subject<Unit> _boardUpdated = new();
    public IObservable<Unit> BoardUpdated => _boardUpdated;

    public AppMatchService(IGameRules rules)
    {
        _rules = rules;
        _board = new Board(19); // Default size
        _currentPlayer = StoneColor.Black;
    }

    public void StartNewGame(int boardSize)
    {
        _board = new Board(boardSize);
        _currentPlayer = StoneColor.Black;
        _positionHistory.Clear();
        GameResult = null;
        _boardUpdated.OnNext(Unit.Default);
    }

    public bool MakeMove(int x, int y)
    {
        // Проверка на завершенную игру
        if (GameResult != null)
            return false;

        // Проверка допустимости хода
        if (!_rules.IsValidMove(_board, x, y, _currentPlayer))
            return false;

        // Создаем копию доски для анализа
        var testBoard = _board.DeepCopy();
        testBoard.PlaceStone(x, y, _currentPlayer);

        // Проверка правила ко
        var positionHash = testBoard.GetPositionHash();
        if (_positionHistory.Contains(positionHash))
            return false;

        // Применяем ход
        _board.PlaceStone(x, y, _currentPlayer);

        // Обработка захвата камней
        var capturedStones = _rules.CheckCaptures(_board, x, y);
        foreach (Stone stone in capturedStones)
        {
            _board.RemoveStone(stone.X, stone.Y);
        }

        // Сохраняем хэш позиции для правила ко
        _positionHistory.Add(_board.GetPositionHash());

        // Проверка окончания игры (если игрок пасует)
        if (x == -1 && y == -1) // Предполагаем, что (-1,-1) - это пас
        {
            if (_consecutivePasses >= 1) // После двух пасов игра заканчивается
            {
                EndGame();
                return true;
            }
            _consecutivePasses++;
        }
        else
        {
            _consecutivePasses = 0;
        }

        // Смена игрока
        _currentPlayer = _currentPlayer.Opposite();
        _boardUpdated.OnNext(Unit.Default);

        return true;
    }

    private int _consecutivePasses = 0;

    private void EndGame()
    {
        // Расчет счета
        var score = _rules.CalculateScore(_board);
        GameResult = _rules.DetermineWinner(score);
        _boardUpdated.OnNext(Unit.Default);
    }
}
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
    
    private Board _board;
    public int BoardWidth { get; set; }
    private StoneColor _currentPlayer;
    private readonly List<ulong> _positionHistory = new();
    
    public Board CurrentBoard => _board;
    public StoneColor CurrentPlayer => _currentPlayer;
    public StoneColor? GameResult { get; private set; }
    
    private readonly Subject<Unit> _boardUpdated = new();
    public IObservable<Unit> BoardUpdated => _boardUpdated;

    

    public void StartNewGame(int boardSize)
    {
        BoardWidth = boardSize;
        _board = new Board(boardSize);
        _currentPlayer = StoneColor.Black;
        _positionHistory.Clear();
        GameResult = null;
        _boardUpdated.OnNext(Unit.Default);
    }

    
}
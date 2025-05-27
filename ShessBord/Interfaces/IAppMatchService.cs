using System;
using System.Reactive;
using ShessBord.Controls;
using ShessBord.Models;

namespace ShessBord.Interfaces;

public interface IAppMatchService
{
    // Properties
    Board CurrentBoard { get; }
    StoneColor CurrentPlayer { get; }
    StoneColor? GameResult { get; }
        
    // Events
    IObservable<Unit> BoardUpdated { get; }
        
    // Methods
    void StartNewGame(int boardSize);
    bool MakeMove(int x, int y);
}
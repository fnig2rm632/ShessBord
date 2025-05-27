using System;
using System.Windows.Input;

namespace ShessBord.Controls.GoBordControl;

internal class RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    : ICommand
{
    private readonly Action<object?> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

    public void Execute(object? parameter)
    {
        _execute(parameter);
        RaiseCanExecuteChanged();
    }

    public event EventHandler? CanExecuteChanged;

    private void RaiseCanExecuteChanged() => 
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
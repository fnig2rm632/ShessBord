using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Media;

namespace ShessBord.Controls.GoBordControl;

public class CellBoard(
    int column,
    int row,
    ICommand command,
    GoGameBoardControl.SelectedSideType sideType,
    char? status = null,
    ThemeBoard? themeBoard = null) : INotifyPropertyChanged
{
    public int Column { get; } = column;
    public int Row { get; } = row;
    public ICommand Command { get; } = command;
    private char? _status;
    public char? Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }
    
    public ThemeBoard? Theme {get; set; } = themeBoard;
    private ThemeCell ThemeCell => ChoseTheme(Status);
    public Color FillCell => Status != null ? ThemeCell.MainColor : Colors.Transparent;
    public Color FillCellSecond => Status != null ? ThemeCell.ShadowColor : Colors.Transparent;
    public SolidColorBrush BackGroundColor => Status != null ? SolidColorBrush.Parse("Black") : SolidColorBrush.Parse("Transparent");
    public GoGameBoardControl.SelectedSideType SideType {get; set; } = sideType;
    public SolidColorBrush HoverColor => SideType == GoGameBoardControl.SelectedSideType.Black ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
    public bool IsVisibleCell => Status == null;
    public decimal FillCellIndex => Status != null ? ThemeCell.PointMain : 1;
    public decimal FillCellIndexSecond => Status != null ? ThemeCell.PointMain : 1;


    private ThemeCell ChoseTheme(char? c) =>  c switch 
    {
        'w' => new ThemeCell((decimal)0.1, Color.Parse("#dadada"), 1, Colors.White),
        'b' => new ThemeCell(0, Colors.White, 1, Colors.Black),
        _ => new ThemeCell((decimal)0.1, Colors.OrangeRed, 1, Colors.Red)
    };
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
    
}
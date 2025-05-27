using Avalonia.Media;

namespace ShessBord.Controls.GoBordControl;

public abstract class ThemeBoard(
    ThemeCell whiteTheme,
    ThemeCell blackTheme,
    Color lineColor,
    Color selectedLineColor,
    Color? backgroundColor)
{
    public ThemeCell? WhiteTheme {get; set;} = whiteTheme;
    public ThemeCell? BlackTheme {get; set;} = blackTheme;
    public Color? BackgroundColor {get; set;} = backgroundColor;
    public Color? LineColor {get; set;} = lineColor;
    public Color? SelectedLineColor {get; set;} = selectedLineColor;
}
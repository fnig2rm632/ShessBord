using Avalonia.Media;

namespace ShessBord.Controls.GoBordControl;

public class ThemeCell(decimal pointShadow, Color shadowColor, decimal pointMain, Color mainColor)
{
    public decimal PointShadow {get; set;} = pointShadow;

    public Color ShadowColor {get; set;} = shadowColor;

    public decimal PointMain {get; set;} = pointMain;

    public Color MainColor {get; set;} = mainColor;
}
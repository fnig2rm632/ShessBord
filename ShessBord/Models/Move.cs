/// <summary>
/// Представляет ход в игре Го
/// </summary>
public class Move
{
    /// <summary> Координата X (столбец) </summary>
    public int X { get; }
    
    /// <summary> Координата Y (строка) </summary>
    public int Y { get; }
    
    /// <summary> Цвет камня (Black/White) </summary>
    public StoneColor Color { get; }
    
    /// <summary> Был ли это пас </summary>
    public bool IsPass { get; set; }

    public Move(int x, int y, StoneColor color)
    {
        X = x;
        Y = y;
        Color = color;
        IsPass = false;
    }
    
    /// <summary> Создание хода-паса </summary>
    public static Move CreatePass(StoneColor color) => 
        new Move(-1, -1, color) { IsPass = true };
    
    public override string ToString() => 
        IsPass ? $"{Color} Pass" : $"{Color} at ({X},{Y})";
}
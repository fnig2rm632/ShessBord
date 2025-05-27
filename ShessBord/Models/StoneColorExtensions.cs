namespace ShessBord.Models;

public static class StoneColorExtensions
{
    public static StoneColor Opposite(this StoneColor color) => 
        color == StoneColor.Black ? StoneColor.White : StoneColor.Black;
}
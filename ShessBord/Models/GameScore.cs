using System.Collections.Generic;

public class GameScore
{
    private readonly Dictionary<StoneColor, float> _scores = new();
    private const float DefaultKomi = 6.5f; // Стандартное значение коми
    
    public GameScore(float komi = DefaultKomi)
    {
        _scores[StoneColor.Black] = 0;
        _scores[StoneColor.White] = 0;
        Komi = komi;
    }
    
    public float Komi { get; } // Только для чтения
    
    public float this[StoneColor color]
    {
        get => _scores[color];
        set => _scores[color] = value;
    }
    
    public float Difference => 
        _scores[StoneColor.Black] - (_scores[StoneColor.White] + Komi);
    
    public void AddKomi()
    {
        _scores[StoneColor.White] += Komi;
    }
    
    public override string ToString() => 
        $"Black: {_scores[StoneColor.Black]}, White: {_scores[StoneColor.White]} (+{Komi})";
}
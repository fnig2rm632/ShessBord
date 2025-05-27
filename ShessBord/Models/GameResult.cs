namespace ShessBord.Models;

public class GameResult
{
    public StoneColor? Winner { get; }
    public float BlackScore { get; }
    public float WhiteScore { get; }
    public string ResultText { get; }
    public bool IsDraw => !Winner.HasValue;

    public GameResult(float blackScore, float whiteScore, 
        StoneColor? winner, string resultText)
    {
        BlackScore = blackScore;
        WhiteScore = whiteScore;
        Winner = winner;
        ResultText = resultText;
    }

    public override string ToString() => ResultText;
}
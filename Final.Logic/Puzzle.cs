namespace Final.Logic;

public class Puzzle
{
     public string PuzzlePrompt {get; }
    public string CorrectAnswer {get;}
    public string Hint {get;}
    public int AttemptsAllowed {get; }

    public Puzzle(string prompt, string answer, string hint, int attempts)
    {
        PuzzlePrompt = prompt;
        CorrectAnswer = answer;
        Hint = hint;
        AttemptsAllowed = attempts;
    }
}
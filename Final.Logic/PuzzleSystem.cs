namespace Final.Logic;

public class PuzzleSystem
{
   private Dictionary<PuzzleType, Puzzle> _puzzles;

   public PuzzleSystem()
    {
        _puzzles = new Dictionary<PuzzleType, Puzzle>
        {
            {
                PuzzleType.Fire_Easy, new Puzzle(
                    "During a fire, smoke rises. To avoid inhaling toxic smoke, you should:\n\nA) Stand up and run to the exit\nB) Stay low and crawl to the exit\nC) Jump out the window immediately\nD) Hide under a table",
                    "B", 
                    "Smoke rises, so stay low near the floor!", 
                    3
                )
            },
            {
                PuzzleType.Fire_Medium, new Puzzle(
                    "Prompt", 
                    "Answer", 
                    "Hint", 
                    3
                )
            },
            {
                PuzzleType.Fire_Hard, new Puzzle(
                    "Prompt", 
                    "Answer", 
                    "Hint", 
                    3
                )
            }    
        };
    }

    public Puzzle GetPuzzle(PuzzleType type)
    {
        return _puzzles[type];
    }

    public bool EvaluateAnswer(Puzzle puzzle, string answer)
    {
        return answer.Trim().ToUpper() == puzzle.CorrectAnswer.ToUpper();
    }
}
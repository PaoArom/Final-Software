namespace Final.Logic;

public class GameManager
{
    private PuzzleSystem _puzzleSystem = new PuzzleSystem();
    public void StartTutorial()
    {
        var tutorial = new TutorialManager();
        tutorial.Run();

        if (tutorial.DidWin())
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n  Tutorial Complete, You are ready to play");
            Console.ResetColor();
            Console.WriteLine("  Press any key to continue...");
            Console.ReadKey(true);
        }
        else if (tutorial.DidLose())
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  You died, Try again from the menu");
            Console.ResetColor();
            Console.WriteLine("  Press any key to continue...");
            Console.ReadKey(true);
        }
    }
    public int StartGame()
    {
        var map = new MapManager(20, 20, 1, 1);
        var inventory = new InventoryManager();

        bool playing = true;
        DateTime startTime = DateTime.Now;
        int totalSeconds = 60; // un minuto
        bool playerWon = false;
        bool keyCollected = false;

        while (playing)
        {
            // Calcular tiempo restante
            int secondsLeft = totalSeconds - (int)(DateTime.Now - startTime).TotalSeconds;

            if (secondsLeft <= 0)
            {
                ShowGameOver();
                playing = false;
                continue;
            }

            int minsLeft = secondsLeft / 60;
            int secsLeft = secondsLeft % 60;

            Console.SetCursorPosition(0, 0);
            map.DisplayMap();
            Console.WriteLine();

            // Timer con color que cambia según el tiempo
            if (secondsLeft <= 10)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (secondsLeft <= 30)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"  ⏱️  Time left: {minsLeft:D2}:{secsLeft:D2}");
            Console.ResetColor();

            if (map.HasKey())
                Console.WriteLine("  🗝️  You have the key!");
            else
                Console.WriteLine("  🔍 Find the key to open the exit!");

            Console.WriteLine("  Use W/A/S/D to move | E = Inventory | Q = Menu");

            // Esperar tecla con timeout de 1 segundo
            if (!Console.KeyAvailable)
            {
                System.Threading.Thread.Sleep(100);
                continue;
            }

            var keyInfo = Console.ReadKey(true);
            string key = keyInfo.Key.ToString().ToUpper();

            if (key == "Q")
            {
                playing = false;
                continue;
            }

            if (key == "E")
            {
                if (map.HasKey() && !inventory.HasItem("🗝️  Old Key"))
                    inventory.PickUpItem("🗝️  Old Key");

                inventory.ShowInventory();
                continue;
            }

            if (key is "W" or "A" or "S" or "D")
            {
                bool hadKeyBefore = map.HasKey();
                
                map.MovePlayer(key);

                // Check if key was just collected
                if (!hadKeyBefore && map.HasKey() && !keyCollected)
                {
                    keyCollected = true;
                    bool puzzleSolved = ShowFirePuzzle();

                    if (!puzzleSolved)
                    {
                        ShowGameOver();
                        playing = false;
                        continue;
                    }
                }

                if (map.DidLose())
                {
                    ShowGameOver();
                    playing = false;
                }
                else if (map.DidWin())
                {
                    ShowLevelCleared();
                    playerWon = true;
                    playing = false;
                }
            }
            else if (key != "E" && key != "Q")
            {
                Console.WriteLine("Invalid key. Use W/A/S/D, E or Q.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }

        return playerWon ? 1 : 0;
    }

    private bool ShowFirePuzzle()
    {
        Console.Clear();
        
        var puzzle = _puzzleSystem.GetPuzzle(PuzzleType.Fire_Easy);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                       🔥 FIRE PUZZLE 🔥                      ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine(puzzle.PuzzlePrompt);
        Console.WriteLine();

        int attempts = 0;
        bool answeredCorrectly = false;

        while (attempts < puzzle.AttemptsAllowed)
        {
            Console.Write($"\nYour answer (A/B/C/D): ");
            string answer = Console.ReadLine() ?? "";

            if (_puzzleSystem.EvaluateAnswer(puzzle, answer))
            {
                answeredCorrectly = true;
                break;
            }

            attempts++;
            int remainingAttempts = puzzle.AttemptsAllowed - attempts;

            if (remainingAttempts > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Incorrect! Try again. Hint: {puzzle.Hint}");
                Console.ResetColor();
            }
        }

        if (answeredCorrectly)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Correct! You can now proceed.");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
            return true;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ The correct answer was: {puzzle.CorrectAnswer}");
            Console.WriteLine("\n🔥 You failed to answer and got caught by the fire!");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
            return false;
        }
    }

    private void ShowGameOver()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine("  ╔══════════════════════════════════════╗");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ║   ██████╗  █████╗ ███╗   ███╗███████╗║");
        Console.WriteLine("  ║  ██╔════╝ ██╔══██╗████╗ ████║██╔════╝║");
        Console.WriteLine("  ║  ██║  ███╗███████║██╔████╔██║█████╗  ║");
        Console.WriteLine("  ║  ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ║");
        Console.WriteLine("  ║  ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗║");
        Console.WriteLine("  ║   ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝║");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ║   ██████╗ ██╗   ██╗███████╗██████╗   ║");
        Console.WriteLine("  ║  ██╔═══██╗██║   ██║██╔════╝██╔══██╗  ║");
        Console.WriteLine("  ║  ██║   ██║██║   ██║█████╗  ██████╔╝  ║");
        Console.WriteLine("  ║  ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗  ║");
        Console.WriteLine("  ║  ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║  ║");
        Console.WriteLine("  ║   ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝  ║");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ║         🔥 You touched fire 🔥       ║");
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ╚══════════════════════════════════════╝");
        Console.WriteLine();
        Console.ResetColor();
        Console.WriteLine("          Press any key to return to the menu...");
        Console.ReadKey(true);
    }

    private void ShowLevelCleared()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine("  ╔═══════════════════════════════════════════╗");
        Console.WriteLine("  ║                                           ║");
        Console.WriteLine("  ║  ██╗     ███████╗██╗   ██╗███████╗██╗     ║");
        Console.WriteLine("  ║  ██║     ██╔════╝██║   ██║██╔════╝██║     ║");
        Console.WriteLine("  ║  ██║     █████╗  ██║   ██║█████╗  ██║     ║");
        Console.WriteLine("  ║  ██║     ██╔══╝  ╚██╗ ██╔╝██╔══╝  ██║     ║");
        Console.WriteLine("  ║  ███████╗███████╗ ╚████╔╝ ███████╗███████╗║");
        Console.WriteLine("  ║  ╚══════╝╚══════╝  ╚═══╝  ╚══════╝╚══════╝║");
        Console.WriteLine("  ║                                           ║");
        Console.WriteLine("  ║   ██████╗██╗     ███████╗ █████╗ ██████╗  ║");
        Console.WriteLine("  ║  ██╔════╝██║     ██╔════╝██╔══██╗██╔══██╗ ║");
        Console.WriteLine("  ║  ██║     ██║     █████╗  ███████║██████╔╝ ║");
        Console.WriteLine("  ║  ██║     ██║     ██╔══╝  ██╔══██║██╔══██╗ ║");
        Console.WriteLine("  ║  ╚██████╗███████╗███████╗██║  ██║██║  ██║ ║");
        Console.WriteLine("  ║   ╚═════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝ ║");
        Console.WriteLine("  ║                                           ║");
        Console.WriteLine("  ║       🚪 You found the exit 🎉            ║");
        Console.WriteLine("  ║                                           ║");
        Console.WriteLine("  ╚═══════════════════════════════════════════╝");
        Console.WriteLine();
        Console.ResetColor();
        Console.WriteLine("          Press any key to return to the menu...");
        Console.ReadKey(true);
    }
}

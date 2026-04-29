namespace Final.Logic;

public class GameManager
{
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
    public void StartGame()
{
    var map       = new MapManager(20, 20, 1, 1);
    var inventory = new InventoryManager();

    bool playing        = true;
    DateTime startTime  = DateTime.Now;
    int totalSeconds    = 60; // un minuto

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

        Console.Clear();
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
        string key  = keyInfo.Key.ToString().ToUpper();

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
            map.MovePlayer(key);

            if (map.DidLose())
            {
                ShowGameOver();
                playing = false;
            }
            else if (map.DidWin())
            {
                ShowLevelCleared();
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
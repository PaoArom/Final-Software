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
    public int StartGame()
    {
        var map = new MapManager(20, 20, 1, 1);
        var inventory = new InventoryManager();

        bool playing = true;
        bool playerWon = false;

        while (playing)
        {
            Console.Clear();
            map.DisplayMap();
            Console.WriteLine();

            if (map.HasKey())
                Console.WriteLine("  🗝️  You have the key");
            else
                Console.WriteLine("  🔍 Find the key to open the exit");

            Console.WriteLine("  Use WASD to move  E for Inventory  Q for Menu");

            var keyInfo = Console.ReadKey(true);
            string key = keyInfo.Key.ToString().ToUpper();

            if (key == "Q")
            {
                playing = false;
                continue;
            }

            if (key == "E")
            {
                if (map.HasKey() && !inventory.HasItem("🗝️  Key"))
                    inventory.PickUpItem("🗝️  Key");

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
                    playerWon = true;
                    playing = false;
                }
            }
            else if (key != "E" && key != "Q")
            {
                Console.WriteLine("Invalid key Use WASD, E or Q.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }

        return playerWon ? 1 : 0; 
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
